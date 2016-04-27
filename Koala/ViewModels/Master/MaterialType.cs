using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Krokot.Database;

namespace Koala.ViewModels.Master
{
    public class MaterialType : BaseGridRow, ISaveCommand
    {
        public event EventHandler ItemChanged;

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
            set
            {
                NotifyIfChanged(ref isReadOnly, value);
            }
        }

        private bool isClone;
        public bool IsClone
        {
            get
            {
                return isClone;
            }
            set
            {
                NotifyIfChanged(ref isClone, value);
            }
        }
        public Visibility UpdateVisibility
        {
            get
            {
                return isReadOnly ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility AddVisibility
        {
            get
            {
                return !isReadOnly ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string QualityId { get; set; }
        public string QualityName { get; set; }
        public KeyValueOption SelectedQuality{get;set;}
        public List<QualityType> Quality { get; set; }
        public System.Windows.Input.ICommand AddCommand { get; set; } 
        public System.Windows.Input.ICommand UpdateCommand { get; set; }
        public MaterialType()
        {
            AddCommand = new DelegateCommand<MaterialType>(new Action<MaterialType>(OnAdd));
            UpdateCommand = new DelegateCommand<MaterialType>(new Action<MaterialType>(OnUpdate));
            Quality = ObjectPool.Instance.Resolve<List<QualityType>>();
        }

        private void OnItemChanged()
        {
            if (ItemChanged != null)
                ItemChanged(this, null);
        }

        private void OnUpdate(MaterialType model)
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
            db.Execute("UpdateMaterial", new
            {
                Id = model.Id,
                Description = model.Description,
                QualityId = model.SelectedQuality.Id,
                Price = model.Price
            });
            db.Close();
            OnItemChanged();
        }

        private void OnAdd(MaterialType model)
        {
            if (model.Description == null || model.SelectedQuality == null || model.Price == 0)
                return;

            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
            db.Execute("InsertMaterial", new
            {
                Id = model.Id,
                Description = model.Description,
                QualityId = model.SelectedQuality.Id,
                Price = model.Price
            });
            db.Close();
            OnItemChanged(); 
        }

        private void GenerateModel()
        {
            this.Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper();
            this.Description = string.Empty;
            this.SelectedQuality = new KeyValueOption();
            this.Price = 0;
        }
       
    }
}
