using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krokot.Database;

namespace Koala.ViewModels.Master
{
    public class SizeCollectionViewSource : BaseMasterCollectionViewModel<KeyValueOption>
    { 
        public override void OnEdit(object arg)
        {
            if (arg != null)
            {
                KeyValueOption model = (KeyValueOption)arg;
                model.IsReadOnly = true;

                IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
                if (dialog.ShowDialog<Size>(model).Value == true)
                {
                    IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                    IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);

                    try
                    {
                        db.Execute("UpdateSize", new
                        {
                            Id = model.Id,
                            Description = model.Description
                        });
                    }
                    catch { }
                    
                    db.Close();
                    OnSourceChanged(this);
                }
            }
        }

        public override void OnDelete(object arg)
        { 
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name); 
            foreach (KeyValueOption item in Source)
            {
                if (item.IsSelected)
                {

                    try
                    {
                        db.Execute("DeleteSize", new
                        {
                            Id = item.Id
                        });
                    }
                    catch { }
                    
                }
            }
            db.Close();
            CheckedHeader = false;
            OnSourceChanged(this);
        }

        public override void OnCreate()
        {
            KeyValueOption model = new KeyValueOption();
            model.Id = GenerateId(3, "SE");
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            if (dialog.ShowDialog<Size>(model).Value == true)
            {
                if (model.Description == null)
                    return;


                this.Source.Add(model);

                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);

                try
                {
                    db.Execute("InsertSize", new
                    {
                        Id = model.Id,
                        Description = model.Description
                    });
                }
                catch { }
                
                db.Close();
                OnSourceChanged(this);
            }
        }
    }
}
