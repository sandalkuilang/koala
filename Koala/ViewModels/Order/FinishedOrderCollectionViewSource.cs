﻿using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Texaco.Database;

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
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
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
             
            //orderCollaborator.IndexRefreshing = 3;
            //orderCollaborator.Pull();
            base.OnDelete(obj);
        }

        public override void OnEditCommand(object obj)
        {
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            CreateOrderModel model = ((CreateOrderModel)obj);
            model.CanEdit = false;
            dialog.ShowDialog<CreateOrder>(model);
        }

        public override void OnPrint(object obj)
        {
            
        }
    }
}