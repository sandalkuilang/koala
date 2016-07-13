using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.ViewModels.Stock;
using Koala.ViewModels.User;
using Krokot.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Master
{
    public class StockCollectionViewSource : BaseMasterCollectionViewModel<TransactionStock>
    {
        public override void OnCreate()
        {
            TransactionStock model = new TransactionStock();
            
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            if (dialog.ShowDialog<Koala.Views.Dialogs.Stock>(model).Value == true)
            {
                if (model.MaterialId == null || model.QualityId == null || model.SupplierId == null || model.Qty == 0)
                    return;
                 
                this.Source.Add(model);

                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);

                try
                {
                    db.Execute("InsertStock", new
                    {
                         
                    });
                }
                catch { }

                db.Close();
                OnSourceChanged(this);
            }
        }

        public override void OnDelete(object arg)
        {
            if (arg != null)
            {
                TransactionStock model = (TransactionStock)arg; 

                IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
                if (dialog.ShowDialog<Koala.Views.Dialogs.Stock>(model).Value == true)
                {
                    IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                    IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
                    UserModel user = ObjectPool.Instance.Resolve<UserModel>();

                    try
                    {
                        db.Execute("DeleteStock", new
                        {
                            MaterialId = model.MaterialId,
                            QualityId = model.QualityId,
                            SupplierId = model.SupplierId,
                            Qty = model.Qty,
                            CreatedBy = user.Username
                        });
                    }
                    catch { }

                    db.Close();
                    OnSourceChanged(this);
                }
            }
        }

        public override void OnEdit(object arg)
        {
            
        }
    }
}
