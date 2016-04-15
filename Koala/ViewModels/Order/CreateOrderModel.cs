using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Texaco.Database;

using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.ComponentModel;

namespace Koala.ViewModels.Order
{
    public class CreateOrderModel : BaseGridRow, IInvoiceCommand, ISaveCommand, IOrderDetailCommand, IDataErrorInfo
    {
        public delegate void OrderEventHandler(object sender, EventArgs e);

        public event OrderEventHandler DataChanged;
        public event OrderEventHandler ItemCreated;
        public event OrderEventHandler StatusCompleted;

        private bool canEdit;
        public bool CanEdit
        {
            get
            {
                canEdit = Status == "I" || Status == "Q";
                return canEdit;
            }
            set
            {
                value = Status == "I" || Status == "Q";
                NotifyIfChanged(ref canEdit, value);
            }
        }

        public bool NewOrder { get; set; }
        private decimal paymentBeforeDiscount;
        
        private bool paid;
        public bool Paid
        {
            get
            {
                return paid;
            }
            set
            {
                NotifyIfChanged(ref paid, value);
            }
        }

        public string CompanyName
        {
            get
            {
                return ApplicationSettings.Instance.Contact.Company;
            }
        }

        public string Address
        {
            get
            {
                return ApplicationSettings.Instance.Contact.Address;
            }
        }
        
        private string customerName;
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                NotifyIfChanged(ref customerName, value);
            }
        }

        private string paymentStatus;
        public string PaymentStatus
        {
            get
            {
                return paymentStatus;
            }
            set
            {
                NotifyIfChanged(ref paymentStatus, value);
            }
        }

        private string customerPhone;
        public string CustomerPhone
        {
            get
            {
                return customerPhone;
            }
            set
            {
                NotifyIfChanged(ref customerPhone, value);
            }
        }

        private string poNumber;
        public string PoNumber
        {
            get
            {
                return poNumber;
            }
            set
            {
                NotifyIfChanged(ref poNumber, value);
            }
        }

        public DateTime UpdateDate { get;set; }

        private DateTime createdDate;
        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                NotifyIfChanged(ref createdDate, value);
            }
        }

        private decimal discount;
        public decimal Discount
        {
            get
            {
                return discount;
            }
            set
            {
                NotifyIfChanged(ref discount, value);
                CalculatingTotalPaymentByDiscount();
            }
        }

        private decimal change;
        public decimal Change
        {
            get
            {
                return change;
            }
            set
            {
                NotifyIfChanged(ref change, value);
            }
        }

        private decimal installment;
        public decimal Installment
        {
            get
            {
                return installment;
            }
            set
            {
                NotifyIfChanged(ref installment, value);
                CalculatingTotalPaymentByDiscount(() => 
                    {
                        paymentBeforeDiscount = 0;
                    });
            }
        }

        private decimal remaining;
        public decimal Remaining
        {
            get
            {
                return remaining;
            }
            set
            {
                NotifyIfChanged(ref remaining, value);
            }
        }

        private decimal totalPayment;
        public decimal TotalPayment
        {
            get
            {
                return totalPayment;
            }
            set
            {
                NotifyIfChanged(ref totalPayment, value);
            }
        }
         
        private string detailHeader;
        public string DetailHeader
        {
            get
            {
                return detailHeader;
            }
            set
            {
                NotifyIfChanged(ref detailHeader, value);
            }
        }
         
        public string Status { get; set; }

        public System.Windows.Input.ICommand AddCommand { get; set; } 
        public System.Windows.Input.ICommand UpdateCommand { get; set; }
        //public System.Windows.Input.ICommand SaveCommand { get; set; }
        public ReactiveCommand<Unit> SaveCommand { get; set; }
        public System.Windows.Input.ICommand PrintCommand { get; set; }
        public System.Windows.Input.ICommand AddDetailCommand { get; set; } 
        public System.Windows.Input.ICommand DeleteDetailCommand { get; set; }
        public System.Windows.Input.ICommand EditDetailCommand { get; set; }
        public System.Windows.Input.ICommand UpdateDetailStatusCommand { get; set; }
        public System.Windows.Input.ICommand StartPrintingCommand { get; set; }


        private CreateOrderDetailCollectionViewSource details;
        public CreateOrderDetailCollectionViewSource Details
        {
            get
            {
                return details;
            }
            set
            {
                NotifyIfChanged(ref details, value);
                NotifyDetailHeader();
            }
        }

        public CreateOrderModel()
        {
            Status = "I";
            NewOrder = true;
            Details = new CreateOrderDetailCollectionViewSource();
            Details.DataChanged += Details_DataChanged;
            //SaveCommand = new DelegateCommand<object>(new Action<object>(OnSave));

            SaveCommand = ReactiveCommand.CreateAsyncTask(async x => 
            {
                await OnSave(x);
            });

            PrintCommand = new DelegateCommand<object>(new Action<object>(OnPrint));
            AddDetailCommand = new DelegateCommand(new Action(OnAddDetail)); 

            DeleteDetailCommand = new DelegateCommand<dynamic>(new Action<dynamic>(OnDeleteDetail));
            EditDetailCommand = new DelegateCommand<object>(new Action<object>(OnEditDetail));
            UpdateCommand = new DelegateCommand<object>(new Action<object>(OnUpdate));
            UpdateDetailStatusCommand = new DelegateCommand<object>(new Action<object>(OnUpdateDetailStatus));
            StartPrintingCommand = new DelegateCommand<Visual>(new Action<Visual>(OnStartPrint));

            CreatedDate = DateTime.Now;
            PoNumber = string.Join("-", 
                Guid.NewGuid().ToString().GetHashCode().ToString("x").ToUpper(), 
                Guid.NewGuid().ToString().GetHashCode().ToString("x").Substring(0,2).ToUpper(),
                Guid.NewGuid().ToString().GetHashCode().ToString("x").Substring(0,1).ToUpper()); 
        }

        private void Details_DataChanged(object sender, MutableObservableCollection<CreateOrderDetailModel> e)
        {
            NotifyDetailHeader();
        }
 
        private void OnUpdateDetailStatus(object status)
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);

            foreach(CreateOrderDetailModel detail in this.details.Source)
            {
                if (detail.IsSelected)
                { 
                    db.Execute("UpdateOrderDetailStatus", new
                    {
                        Queue = status,
                        OrderId = this.poNumber,
                        SeqNbr = detail.SeqNbr
                    });

                    detail.Queue = Convert.ToInt32(status); 
                    detail.QueueStatus = "Just Trigger";
                    detail.IsSelected = false;
                } 
                
            }
            db.Close();

            bool completed = this.details.Source.Where(x => x.Queue == 2).Count() == this.details.Source.Count;
            if (completed)
                OnStatusCompleted();

            this.Details.CheckedHeader = false;
        }

        private void OnUpdate(object obj)
        {
            if (obj == null)
                return;

            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);

            db.Execute("UpdateOrder", new 
            {
                Status = this.Status,
                Total = this.totalPayment,
                Installment = this.installment,
                Remaining = this.remaining,
                Disc = this.discount,
                OrderId = this.poNumber,
            });

            db.Close();
        }

        private void CalculatingTotalPayment()
        {
            CreateOrderDetailModel detail;
            //installment = 0;
            //discount = 0;
            totalPayment = 0;
            paymentBeforeDiscount = 0;
            // remaining must be set last
            Remaining = 0;
            for (int i = 0; i < this.Details.Source.Count; i++)
            {
                detail = this.Details.Source[i];
                this.TotalPayment += detail.Price;
            } 
        }

        private void OnDataChanged()
        {
            if (DataChanged != null)
                DataChanged(this, null);
        }

        private void OnStatusCompleted()
        {
            if (StatusCompleted != null)
                StatusCompleted(this, null);
        }

        private void OnItemCreated()
        {
            if (ItemCreated != null)
                ItemCreated(this, null);
        }

        private void CalculatingTotalPaymentByDiscount(Action calculate = null)
        {
            if (calculate != null)
                calculate.Invoke(); 

            if (paymentBeforeDiscount == 0)
                paymentBeforeDiscount = totalPayment;
             

            if (discount > 0)
            {  
                Remaining = paymentBeforeDiscount - ((paymentBeforeDiscount) * (discount / 100)); 
            }  
            else
            {
                Remaining = paymentBeforeDiscount; 
                Change = 0;
            }
                
            if (installment > 0)
            { 
                if (discount > 0)
                {
                    Remaining = (remaining - installment); 
                }
                else
                {
                    Remaining = (paymentBeforeDiscount - installment) ; 
                }
            }
                
            if (remaining > 0)
            {
                Remaining *= -1;
            }

            if (installment > totalPayment)
            {
                Change = (installment - TotalPayment);
                Remaining = 0;
            }
            else if (installment > 0)
            {
                Change = 0; 
            }
            Paid = totalPayment > 0 && (remaining == 0);


            if (Remaining == 0)
                PaymentStatus = "Paid in Full";
            else
                PaymentStatus = "Installment";

        }

        private void OnEditDetail(object obj)
        {
            if (obj == null)
                return;

            if (((CreateOrderDetailModel)obj).Status == "F")
                return;

            CreateOrderDetailModel model = (CreateOrderDetailModel)obj;
            model.IsReadOnly = true;

            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            model.UpdatingOrderDetail += detail_UpdatingOrderDetail;
            dialog.ShowDialog<CreateOrderDetail>(model);
            model.UpdatingOrderDetail -= detail_UpdatingOrderDetail; 
        }

        void detail_AddingOrderDetail(object sender, CreateOrderDetailModel e)
        {
            Details.Source.Add(e);
            CalculatingTotalPayment();
            CalculatingTotalPaymentByDiscount();
            NotifyDetailHeader();
        }

        void detail_UpdatingOrderDetail(object sender, CreateOrderDetailModel e)
        {
            CalculatingTotalPayment();
            CalculatingTotalPaymentByDiscount();
            NotifyDetailHeader();
        }

        private void NotifyDetailHeader()
        {
            DetailHeader = string.Format("{0} details", Details.Source.Count);
        }

        private void OnDeleteDetail(dynamic obj)
        {
            System.Collections.IList items = (System.Collections.IList)obj;
            var collection = items.Cast<CreateOrderDetailModel>();

            if (!NewOrder)
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
                foreach (CreateOrderDetailModel item in collection.ToList())
                {
                    db.Execute("DeleteOrderDetail", new 
                    {
                        OrderId = item.OrderId,
                        SeqNbr = item.SeqNbr
                    });
                } 
                db.Close();
            }
            
            foreach (CreateOrderDetailModel item in collection.ToList())
            {
                if (item.IsSelected)
                    Details.Source.Remove(item);
            }
            CalculatingTotalPayment();
            CalculatingTotalPaymentByDiscount();
            Details.CheckedHeader = false;
        }

        private void OnAddDetail()
        {
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            CreateOrderDetailModel detail = new CreateOrderDetailModel();

            detail.AddingOrderDetail += detail_AddingOrderDetail;
            bool? dialogResult = dialog.ShowDialog<CreateOrderDetail>(detail);
            detail.AddingOrderDetail -= detail_AddingOrderDetail; 
        }

        private void OnPrint(object obj)
        {
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            dialog.ShowDialog<Koala.Views.Dialogs.PrintInvoice>((CreateOrderModel)obj);
        }

        private void OnStartPrint(Visual obj)
        {            
            Printer.Print((FrameworkElement)obj);
        }

        private async Task OnSave(object obj)
        {
            if (obj != null)
            {
                //Task.Factory.StartNew(() => 
                await Task.Run(() =>
                {
                    IsBusy = true;
                    IsEnabled = false;

                    CreateOrderModel model = (CreateOrderModel)obj;

                    IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                    IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);

                    Dictionary<string, string> scripts = new Dictionary<string, string>();

                    List<int> result = db.Query<int>("CheckExistsOrder", new
                    {
                        OrderId = model.PoNumber
                    });

                    if (result.Any())
                    { 
                        scripts.Add("Order", "UpdateOrder");
                        scripts.Add("OrderDetail", "UpdateOrderDetail");
                    }
                    else
                    {
                        scripts.Add("Order", "CreateOrder");
                        scripts.Add("OrderDetail", "CreateOrderDetail"); 
                    } 

                    IDbTransaction transaction = ((BaseDbCommand)db).BeginTransaction();
                    try
                    {
                        db.Execute(scripts["Order"], new
                        {
                            OrderId = model.PoNumber,
                            CustomerName = model.CustomerName,
                            CustomerPhone = model.CustomerPhone,
                            Status = "I",
                            Total = model.TotalPayment,
                            Installment = model.Installment,
                            Remaining = model.Remaining,
                            Disc = model.discount
                        });

                        CreateOrderDetailModel detail;
                        for (int i = 0; i < model.Details.Source.Count; i++)
                        {
                            detail = model.Details.Source[i];

                            db.Execute(scripts["OrderDetail"], new
                            {
                                OrderId = model.PoNumber,
                                SeqNbr = i + 1,
                                MaterialTypeId = detail.MaterialId,
                                QualityId = detail.QualityId,
                                FinishingId = detail.FinishingId,
                                Title = detail.Title,
                                Width = detail.Width,
                                Height = detail.Height,
                                Qty = detail.Qty,
                                Filename = detail.Filename,
                                Image = detail.Stream,
                                Queue = 0,
                                Deadline = detail.Deadline,
                                Description = detail.Description,
                                Total = detail.Price
                            });
                        } 
                        transaction.Commit();
                    }
                    catch(Exception x)
                    {
                        transaction.Rollback();
                    }
                    
                    db.Close();

                    IsBusy = false;
                    IsEnabled = true;
                     
                    OnItemCreated();
                });

            }
        }
         
        public string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                string messageFormat = "{0} cannot be empty";
                switch (columnName)
                {
                    case "CustomerName":
                        if (this.CustomerName == null)
                        {
                            errorMessage = String.Format(messageFormat, "Name");
                        }
                        break; 
                }
                return errorMessage;
            }
        }
    }

}
