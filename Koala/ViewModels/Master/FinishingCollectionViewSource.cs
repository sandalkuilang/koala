﻿using Koala.Core;
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
                    IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
                    try
                    {
                        db.Execute("UpdateFinishing", new
                        {
                            Id = model.Id,
                            Description = model.Description
                        });
                    }
                    catch { }
                    {

                    }
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
                        db.Execute("DeleteFinishing", new
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
            model.Id = GenerateId(3, "FN");
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            if (dialog.ShowDialog<Finishing>(model).Value == true)
            {
                if (model.Description == null)
                    return;

                this.Source.Add(model);

                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);

                try
                {
                    db.Execute("InsertFinishing", new
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
