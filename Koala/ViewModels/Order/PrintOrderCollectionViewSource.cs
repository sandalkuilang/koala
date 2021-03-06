﻿using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Krokot.Database;
using Koala.ViewModels.User;

namespace Koala.ViewModels.Order
{
    public class PrintOrderCollectionViewSource : BaseOrderCollectionViewModel<CreateOrderModel>
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

        public PrintOrderCollectionViewSource()
        {
            Source = new MutableObservableCollection<CreateOrderModel>(); 
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
            OrderCollaborator orderCollaborator = ObjectPool.Instance.Resolve<OrderCollaborator>(); 
            UserModel user = ObjectPool.Instance.Resolve<UserModel>();
            foreach (CreateOrderModel order in Source.ToList())
            {
                if (order.IsSelected)
                {
                    db.Execute("UpdatePrintOrder", new
                    {
                        OrderId = order.PoNumber,
                        Status = status,
                        Queue = 1
                    });
                    order.IsSelected = false;

                    /// update list
                    order.Status = "Q";
                     
                    foreach (CreateOrderDetailModel detail in order.Details.Source)
                    {
                        db.Execute("InsertTransactionStock", new
                        {
                            MaterialId = detail.MaterialId,
                            QualityId = detail.QualityId,
                            Qty = detail.Qty,
                            CreatedBy = user.Username,
                        });
                    }

                    orderCollaborator.PrintOrder.source.Remove(order);
                    orderCollaborator.Queue.Source.Add(order);
                }
            }
            db.Close(); 
            orderCollaborator.Queue.Source = orderCollaborator.Queue.Source.OrderBy(x => x.UpdateDate).Convert();
            CheckedHeader = false;
        }

        public override void OnEditCommand(object obj)
        {
            if (obj != null)
            {
                IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();

                ((CreateOrderModel)obj).DataChanged += PrintOrderCollectionViewSource_DataChanged;
                if (dialog.ShowDialog<CreateOrder>(obj).Value == true)
                {
                    //OrderCollaborator orderCollaborator = ObjectPool.Instance.Resolve<OrderCollaborator>();
                    //orderCollaborator.IndexRefreshing = 1;
                    //orderCollaborator.Pull();   
                }
                ((CreateOrderModel)obj).DataChanged -= PrintOrderCollectionViewSource_DataChanged;
            }
        }

        void PrintOrderCollectionViewSource_DataChanged(object sender, EventArgs e)
        {
            CreateOrderModel model = ((CreateOrderModel)sender);
            model.DataChanged -= PrintOrderCollectionViewSource_DataChanged;
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
                OrderCollaborator orderCollaborator = ObjectPool.Instance.Resolve<OrderCollaborator>();

                foreach (CreateOrderModel order in Source.ToList())
                {
                    if (order.IsSelected)
                    {
                        db.Execute("DeletePrintOrder", new
                        {
                            OrderId = order.PoNumber,
                            Status = "I"
                        });
                        orderCollaborator.PrintOrder.Source.Remove(order);
                    }
                }
                db.Close();
                base.OnDelete(arg);
            } 
        }

        public override void OnPrint(object arg)
        { 
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            dialog.ShowDialog<Koala.Views.Dialogs.PrintInvoice>((CreateOrderModel)arg);
        }  
         
    }
}
