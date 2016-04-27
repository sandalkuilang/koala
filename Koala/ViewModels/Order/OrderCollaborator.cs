using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.ViewModels.Master;
using Koala.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Krokot.Database; 
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace Koala.ViewModels.Order
{
    public class OrderCollaborator : BaseBindableModel, ISynchronize
    {
        private bool run;
        public ICommand CreateOrderCommand { get; set; }
        public ReactiveCommand<Unit> RefreshCommand { get; set; }

        public List<KeyValueOption> Range { get; set; }

        private KeyValueOption selectedRange;
        public KeyValueOption SelectedRange
        {
            get
            {
                return selectedRange;
            }
            set
            {
                NotifyIfChanged(ref selectedRange, value);
                if (value == null)
                    SelectedRange = Range[1];
            }
        }
         
        private PrintOrderCollectionViewSource printOrder;

        public int IndexRefreshing { get; set; }

        public PrintOrderCollectionViewSource PrintOrder
        {
            get
            {
                return printOrder;
            }
            set
            {
                NotifyIfChanged(ref printOrder, value);
            }
        }

        private FinishedOrderCollectionViewSource paymentOrder;
        public FinishedOrderCollectionViewSource PaymentOrder
        {
            get
            {
                return paymentOrder;
            }
            set
            {
                NotifyIfChanged(ref paymentOrder, value);
            }
        }

        private QueueCollectionViewSource queue;
        public QueueCollectionViewSource Queue
        {
            get
            {
                return queue;
            }
            set
            {
                NotifyIfChanged(ref queue, value);
            }
        }

        private string searchInput;
        public string SearchInput
        {
            get
            {
                return searchInput;
            }
            set
            {
                NotifyIfChanged(ref searchInput, value);
            }
        }

        public ICommand SearchCommand { get; set; }

        public OrderCollaborator()
        {
            IndexRefreshing = -1;
            Range = new List<KeyValueOption>();
            Range.Add(new KeyValueOption() 
            {
                Id = "1",
                Description = "1 month"
            });

            Range.Add(new KeyValueOption()
            {
                Id = "3",
                Description = "3 months ago"
            });

            Range.Add(new KeyValueOption()
            {
                Id = "6",
                Description = "6 months ago"
            });

            Range.Add(new KeyValueOption()
            {
                Id = "12",
                Description = "1 year ago"
            });

            Range.Add(new KeyValueOption()
            {
                Id = "24",
                Description = "2 years ago"
            });

            Range.Add(new KeyValueOption()
            {
                Id = "24",
                Description = "3 years ago"
            });

            Range.Add(new KeyValueOption()
            {
                Id = UInt16.MaxValue.ToString(),
                Description = "All transaction"
            });


            PrintOrder = new PrintOrderCollectionViewSource();
            PaymentOrder = new FinishedOrderCollectionViewSource();
            Queue = new QueueCollectionViewSource();

            PrintOrderMaster = new PrintOrderCollectionViewSource();
            PaymentOrderMaster = new FinishedOrderCollectionViewSource();
            QueueMaster = new QueueCollectionViewSource();
             
            CreateOrderCommand = new DelegateCommand(new Action(OnCreateOrder));
            //RefreshCommand = new DelegateCommand(new Action(OnRefresh));
            RefreshCommand = ReactiveCommand.CreateAsyncTask(async _ => 
            {
                IndexRefreshing = -1;
                await OnRefresh();
            });
           
            SearchCommand = new DelegateCommand(new Action(OnSearch));

            //Task.Factory.StartNew(() => 
            //{
            //    Thread.Sleep(0);
            RefreshCommand.Execute(null);
            //});

            SelectedRange = Range.SingleOrDefault(x => x.Id == "3");
        }

        void Source_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
             
        }

        private PrintOrderCollectionViewSource PrintOrderMaster;
        private QueueCollectionViewSource QueueMaster;
        private FinishedOrderCollectionViewSource PaymentOrderMaster;
        private void OnSearch()
        {
            //Task.Factory.StartNew(() =>
            //{
            PrintOrder.Source = OrderSearchEngine.Filter(PrintOrderMaster.Source, searchInput);
            PaymentOrder.Source = OrderSearchEngine.Filter(PaymentOrderMaster.Source, searchInput);
            Queue.Source = OrderSearchEngine.Filter(QueueMaster.Source, searchInput);
            //}).ContinueWith(x =>
            //{
            //if (string.IsNullOrEmpty(searchInput))
            //    Pull();
            //});
        }

        private async Task OnRefresh()
        {
            await Task.Run(() =>
            {
                // Initialize master 
                IsBusy = true; 
                IsEnabled = false;
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
                List<QualityType> quality = db.Query<QualityType>("GetQuality");
                ObjectPool.Instance.Register<List<QualityType>>().ImplementedBy(quality);
                db.Close();
                if (selectedRange == null)
                    selectedRange = Range[1];
                Pull();
                IsBusy = false;
                IsEnabled = true;
            });
        }

        private void OnCreateOrder()
        {
            CreateOrderModel order = new CreateOrderModel();
            IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
            
            order.ItemCreated += order_ItemCreated;
            dialog.ShowDialog<CreateOrder>(order);
        }

        void order_ItemCreated(object sender, EventArgs e)
        {
            CreateOrderModel model = ((CreateOrderModel)sender);
            PrintOrder.Source.Add((CreateOrderModel)sender);
            PrintOrder.Source = PrintOrder.Source.OrderByDescending(x => x.CreatedDate).Convert();
            model.ItemCreated -= order_ItemCreated;
        }


        public void Pull()
        {
            //Task.Factory.StartNew(() => 
            //{
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
            MutableObservableCollection<CreateOrderModel> newPaymentOrder = new MutableObservableCollection<CreateOrderModel>();
            MutableObservableCollection<CreateOrderModel> newPrintOrder  = new MutableObservableCollection<CreateOrderModel>();
            MutableObservableCollection<CreateOrderModel> newQueue = new MutableObservableCollection<CreateOrderModel>();
            KeyValueOption selectedRange = this.SelectedRange;

            if (IndexRefreshing == -1 || IndexRefreshing == 1)
            {
                newPrintOrder = new MutableObservableCollection<CreateOrderModel>();
                newPrintOrder = db.Query<CreateOrderModel>("GetPrintOrder", new { Between = Convert.ToInt32(selectedRange.Id) * -1 }).Convert<CreateOrderModel>();
                PrintOrderMaster.Source = newPrintOrder.Convert<CreateOrderModel>();
                GetDetails(PrintOrderMaster.Source, db);
            }
            if (IndexRefreshing == -1 || IndexRefreshing == 2)
            {
                newPaymentOrder = new MutableObservableCollection<CreateOrderModel>();
                newPaymentOrder = db.Query<CreateOrderModel>("GetFinishedOrder", new { Between = Convert.ToInt32(selectedRange.Id) * -1 }).Convert<CreateOrderModel>();
                PaymentOrderMaster.Source = newPaymentOrder.Convert<CreateOrderModel>();
                GetDetails(PaymentOrderMaster.Source, db);
            }
            if (IndexRefreshing == -1 || IndexRefreshing == 3)
            {
                newQueue = new MutableObservableCollection<CreateOrderModel>();
                newQueue = db.Query<CreateOrderModel>("GetQueue", new { Between = Convert.ToInt32(selectedRange.Id) * -1 }).Convert<CreateOrderModel>();
                QueueMaster.Source = newQueue.Convert<CreateOrderModel>();
                GetDetails(QueueMaster.Source, db);
            }

            if (string.IsNullOrEmpty(searchInput))
            {
                if (IndexRefreshing == -1 || IndexRefreshing == 1)
                {
                    PaymentOrder.CheckedHeader = false;
                    PrintOrder.Source = newPrintOrder;
                    GetDetails(PrintOrder.Source, db);
                }
                if (IndexRefreshing == -1 || IndexRefreshing == 1)
                {
                    PrintOrder.CheckedHeader = false;
                    PaymentOrder.Source = newPaymentOrder;
                    GetDetails(PaymentOrder.Source, db);
                }
                if (IndexRefreshing == -1 || IndexRefreshing == 1)
                {
                    Queue.CheckedHeader = false;
                    Queue.Source = newQueue;
                    GetDetails(Queue.Source, db); 
                } 
            }

            db.Close(); 
           // }); 
        }

        private void GetDetails(MutableObservableCollection<CreateOrderModel> source, IDataCommand db)
        {
            List<CreateOrderDetailModel> details;
            foreach (CreateOrderModel order in source.ToList())
            {
                order.NewOrder = false;
                details = db.Query<CreateOrderDetailModel>("GetOrderDetail", new { OrderId = order.PoNumber });
                if (details.Any())
                {
                    order.Details.Source = details.Convert<CreateOrderDetailModel>();
                    foreach (CreateOrderDetailModel detail in order.Details.Source.ToList())
                    {
                        detail.Finishing = db.Query<KeyValueOption>("GetFinishing");
                        detail.Size = db.Query<KeyValueOption>("GetSize");
                        detail.Quality = ObjectPool.Instance.Resolve<List<QualityType>>();
                        detail.MaterialMaster = db.Query<MaterialType>("GetMaterial");
                        detail.Material = detail.MaterialMaster.GroupBy(x => x.Id).Select(d => d.First()).ToList(); 
                    }
                }
            }  
        }

        public void Push()
        {
            
        }


        public void Start()
        {
            run = true;
            Task.Factory.StartNew(() =>
            {
                while (run)
                {
                    Pull();
                    Thread.Sleep(10000);
                }
            });
        }

        public void Stop()
        {
            run = false;
        }
    }
}
