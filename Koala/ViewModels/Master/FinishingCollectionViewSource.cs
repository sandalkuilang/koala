using Koala.Core;
using Koala.Views.Dialogs;
using Krokot.Database;
using Koala.ViewModels.Configuration.Client;

namespace Koala.ViewModels.Master
{
    public class FinishingCollectionViewSource : BaseMasterCollectionViewModel<KeyValueOption>
    { 
        public override void OnEdit(object arg)
        {
            if (arg != null)
            {
                KeyValueOption model = (KeyValueOption)arg;
                model.IsReadOnly = true;

                IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
                if (dialog.ShowDialog<Finishing>(model).Value == true)
                {
                    IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                    IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
                    db.Execute("UpdateFinishing", new
                    {
                        Id = model.Id,
                        Description = model.Description
                    });
                    db.Close();
                    OnSourceChanged(this);
                }
            }
        }

        public override void OnDelete(object arg)
        { 
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name); 
            foreach (KeyValueOption item in Source)
            {
                if (item.IsSelected)
                {
                    db.Execute("DeleteFinishing", new
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
            KeyValueOption model = new KeyValueOption();
            model.Id = GenerateId(5);
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            if (dialog.ShowDialog<Finishing>(model).Value == true)
            {
                if (model.Description == null)
                    return;

                this.Source.Add(model);

                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
                db.Execute("InsertFinishing", new
                {
                    Id = model.Id,
                    Description = model.Description
                });
                db.Close();
                OnSourceChanged(this);
            }
        }
    }
}
