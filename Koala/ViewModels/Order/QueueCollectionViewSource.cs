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
    public class QueueCollectionViewSource : BaseOrderCollectionViewModel<CreateOrderModel>
    {
        public ReactiveUI.ReactiveCommand<object> UpdateQueueOrderCommand { get; set; }

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

        public QueueCollectionViewSource()
        {
            Source = new MutableObservableCollection<CreateOrderModel>();
            //UpdateQueueOrderCommand = new DelegateCommand<string>(new Action<string>(OnUpdateQueueOrder));
            UpdateQueueOrderCommand = ReactiveUI.ReactiveCommand.Create();
            UpdateQueueOrderCommand.Subscribe(x => 
            {
                OnUpdateQueueOrder(x.ToString());
            });

        }

        private void OnUpdateQueueOrder(string status)
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
            //bool updated = false;

            OrderCollaborator orderCollaborator = ObjectPool.Instance.Resolve<OrderCollaborator>();  
            foreach (CreateOrderModel order in Source.ToList())
            {
                if (order.IsSelected)
                {
                    if (order.Remaining < 0)
                    {
                        IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
                        bool? dialogResult = dialog.ShowDialog<Warning>(new WarningModel()
                        {
                            Message = string.Format("Po Number {0} - ({1}) must be in complete payment.", order.PoNumber, order.CustomerName)
                        });
                    }
                    else
                    { 
                        //updated = true;
                        db.Execute("UpdatePrintOrder", new
                        {
                            OrderId = order.PoNumber,
                            Status = status,
                            Queue = 2
                        });
                        orderCollaborator.Queue.Source.Remove(order);
                        orderCollaborator.PaymentOrder.Source.Add(order);
                    }
                    order.IsSelected = false;
                }
            }
            db.Close(); 
            orderCollaborator.PaymentOrder.Source = orderCollaborator.PaymentOrder.Source.OrderByDescending(x => x.UpdateDate).Convert(); 
            CheckedHeader = false;
        }

        public override void OnEditCommand(object obj)
        {
            if (obj == null)
                return;
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            ((CreateOrderModel)obj).StatusCompleted += QueueCollectionViewSource_StatusCompleted;
            if (dialog.ShowDialog<CreateOrder>(obj).Value == true)
            {
                //OrderCollaborator orderCollaborator = ObjectPool.Instance.Resolve<OrderCollaborator>();
                //orderCollaborator.IndexRefreshing = 2;
                //orderCollaborator.Pull();
            }
            ((CreateOrderModel)obj).StatusCompleted -= QueueCollectionViewSource_StatusCompleted;
        }

        void QueueCollectionViewSource_StatusCompleted(object sender, EventArgs e)
        {
             
        }

        public override void OnDelete(object obj)
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
                        Status = "Q"
                    });
                    orderCollaborator.Queue.source.Remove(order);
                }
            }
            db.Close();
            //OrderCollaborator orderCollaborator = ObjectPool.Instance.Resolve<OrderCollaborator>();
            //orderCollaborator.IndexRefreshing = 2;
            //orderCollaborator.Pull();
            base.OnDelete(obj);
        }

        public override void OnPrint(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
