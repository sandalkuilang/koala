using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Krokot.Database;

namespace Koala.ViewModels.Order
{
    public class FinishedOrderCollectionViewSource : BaseOrderCollectionViewModel<CreateOrderModel>
    {         

        private MutableObservableCollection<CreateOrderModel> source;
        public override MutableObservableCollection<CreateOrderModel> Source
        {
            get
            {
                return source;
            }
            set
            {
                NotifyIfChanged(ref source, value);
            }
        }

        public FinishedOrderCollectionViewSource()
        {
            Source = new MutableObservableCollection<CreateOrderModel>(); 
        }

        public override void OnDelete(object obj)
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
                OrderCollaborator orderCollaborator = ObjectPool.Instance.Resolve<OrderCollaborator>();
                foreach (CreateOrderModel order in Source.ToList())
                {
                    if (order.IsSelected)
                    {
                        db.Execute("DeletePrintOrder", new
                        {
                            OrderId = order.PoNumber,
                            Status = "F"
                        });
                        orderCollaborator.PaymentOrder.source.Remove(order);
                    }
                }
                db.Close(); 
                base.OnDelete(obj);
            }  
        }

        public override void OnEditCommand(object obj)
        {
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            CreateOrderModel model = ((CreateOrderModel)obj);
            if (model != null)
            {
                model.CanEdit = false;
                dialog.ShowDialog<CreateOrder>(model);
            }
        }

        public override void OnPrint(object obj)
        {
            
        }
    }
}
