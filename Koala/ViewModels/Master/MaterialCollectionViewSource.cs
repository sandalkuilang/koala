using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Texaco.Database;

namespace Koala.ViewModels.Master
{
    public class MaterialCollectionViewSource : BaseMasterCollectionViewModel<MaterialType>
    {
        public ICommand CloneCommand { get; set; }
        public MaterialCollectionViewSource()
        {

            CloneCommand = new DelegateCommand<object>(new Action<object>(OnClone));
        }

        private MaterialType PrepareClone(object obj)
        {
            MaterialType model = (MaterialType)obj;

            MasterCollaborator master = ObjectPool.Instance.Resolve<MasterCollaborator>();
            IEnumerable<MaterialType> initialQuality = master.Material.Source.Where(x => x.Description == model.Description).Select(s => s);
            List<QualityType> exceptQuality = new List<QualityType>();
            foreach (MaterialType material in initialQuality)
            {
                exceptQuality.Add(model.Quality.Where(x => x.Description == material.QualityName).SingleOrDefault());
            }

            model.Id = model.Id;
            model.Description = model.Description;
            model.Quality = model.Quality.Except(exceptQuality).ToList();
            model.IsClone = true;
            model.Price = 0;
            return model;
        }

        private void OnClone(object obj)
        {
            if (obj != null)
            {
                MaterialType model = PrepareClone(obj);

                IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
                model.ItemChanged += model_ItemChanged;
                dialog.ShowDialog<Material>(model); 
                model.ItemChanged -= model_ItemChanged;   
            }
        }

        public override void OnEdit(object arg)
        {
            if (arg != null)
            {
                MaterialType model = (MaterialType)arg;
                model.IsReadOnly = true;
                model.SelectedQuality = model.Quality.Where(x => x.Id == model.QualityId).SingleOrDefault();

                IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
                model.ItemChanged += model_ItemChanged;
                dialog.ShowDialog<Material>(model); 
                model.ItemChanged -= model_ItemChanged;
            }
        }

        public override void OnDelete(object arg)
        { 
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
            foreach (MaterialType item in Source)
            {
                if (item.IsSelected)
                {
                    db.Execute("DeleteMaterial", new
                    {
                        Id = item.Id
                    });
                }
            }
            db.Close();
            CheckedHeader = false;
            OnSourceChanged(this);
        }

        public override void OnCreate()
        {
            MaterialType model = new MaterialType();
            model.Id = GenerateId(5);

            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            model.ItemChanged += model_ItemChanged;
            dialog.ShowDialog<Material>(model);
            model.ItemChanged -= model_ItemChanged; 
        }

        private void model_ItemChanged(object sender, EventArgs e)
        {
            OnSourceChanged(this);
        }
    }
}
