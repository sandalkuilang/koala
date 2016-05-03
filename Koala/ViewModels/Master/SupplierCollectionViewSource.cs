using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.ViewModels.Order;
using Koala.Views.Dialogs;
using Krokot.Database;

namespace Koala.ViewModels.Master
{
    public class SupplierCollectionViewSource : BaseMasterCollectionViewModel<Supplier>
    { 
        public override void OnCreate()
        {
            Supplier model = new Supplier();
            model.SupplierId = GenerateId(3, "SP");
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            if (dialog.ShowDialog<Views.Dialogs.Supplier>(model).Value == true)
            {
                if (model.Name == null || model.Address == null || model.Telp == null)
                    return;


                this.Source.Add(model);

                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);

                try
                {
                    db.Execute("InsertSupplier", new
                    {
                        SupplierId = model.SupplierId,
                        Name = model.Name,
                        Address = model.Address,
                        Telp = model.Telp,
                    });
                }
                catch { }

                db.Close();
                OnSourceChanged(this);
            }
        }

        public override void OnDelete(object arg)
        { 
            WarningModel message = new WarningModel()
            {
                Message = "Are you sure want to delete data?"
            };

            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            bool? result = dialog.ShowDialog<YesNo>(message);

            if (result.HasValue && result.Value)
            { 
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
                foreach (Supplier item in Source)
                {
                    if (item.IsSelected)
                    {

                        try
                        {
                            db.Execute("DeleteSupplier", new
                            {
                                SupplierId = item.SupplierId
                            });
                        }
                        catch { }

                    }
                }
                db.Close();
                CheckedHeader = false;
                OnSourceChanged(this);
            } 
        }

        public override void OnEdit(object arg)
        {
            if (arg != null)
            {
                Supplier model = (Supplier)arg;

                IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
                if (dialog.ShowDialog<Views.Dialogs.Supplier>(model).Value == true)
                {
                    IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                    IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);

                    try
                    {
                        db.Execute("UpdateSupplier", new
                        {
                            SupplierId = model.SupplierId,
                            Name = model.Name,
                            Address = model.Address,
                            Telp = model.Telp
                        });
                    }
                    catch { }

                    db.Close();
                    OnSourceChanged(this);
                }
            }
        }

    }
}
