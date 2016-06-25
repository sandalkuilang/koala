using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Krokot.Database;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Koala.ViewModels.Stock
{
    public class StockCollaborator : BaseBindableModel, ISynchronize
    {
        private bool run;
        private StockType stockTypeSync;
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

        private SummaryCollectionViewSource summary;
        public SummaryCollectionViewSource Summary
        {
            get
            {
                return summary;
            }
            set
            {
                NotifyIfChanged(ref summary, value);
            }
        }

        private TransactionStockViewSource transaction;
        public TransactionStockViewSource Transaction
        {
            get
            {
                return transaction;
            }
            set
            {
                NotifyIfChanged(ref transaction, value);
            }
        }

        public StockCollaborator()
        {
            Summary = new SummaryCollectionViewSource();
            Summary.SourceChanged += Summary_SourceChanged;
            Transaction = new TransactionStockViewSource();
            Transaction.SourceChanged += Transaction_SourceChanged;

            InitializeRange();

            stockTypeSync = StockType.All;

            RefreshCommand = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                stockTypeSync = StockType.Summary;
                await OnRefresh();
            });

            SearchCommand = new DelegateCommand(new Action(OnSearch));

            RefreshCommand.Execute(null);

            SelectedRange = Range.SingleOrDefault(x => x.Id == "3");
        }

        private async Task OnRefresh()
        {
            await Task.Run(() =>
            {
                // Initialize master 
                IsBusy = true;
                IsEnabled = false;
                
                if (selectedRange == null)
                    selectedRange = Range[1];

                Pull();
                IsBusy = false;
                IsEnabled = true;
            });
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

        private void InitializeRange()
        {
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

        }

        private void Transaction_SourceChanged(object sender, EventArgs e)
        {
            stockTypeSync = StockType.Transaction;
            Pull();
        }

        private void Summary_SourceChanged(object sender, EventArgs e)
        {
            stockTypeSync = StockType.Summary;
            Pull();
        }

        public void Pull()
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
            //int stockType = -1;
            //if (stockTypeSync == StockType.All)
            //    stockType = 100;

            //if (stockType == 100 || stockTypeSync == StockType.Summary)
            //    Summary.Source = db.Query<TransactionStock>("GetSummary", new { Between = Convert.ToInt32(this.SelectedRange.Id) * -1 }).Convert<TransactionStock>();

            //if (stockType == 100 || stockTypeSync == StockType.Transaction)
            //    Transaction.Source = db.Query<TransactionStock>("GetTransactionStock", new { Between = Convert.ToInt32(this.SelectedRange.Id) * -1 }).Convert<TransactionStock>();

            Summary.Source = db.Query<TransactionStock>("GetSummary", new { Between = Convert.ToInt32(this.SelectedRange.Id) * -1 }).Convert<TransactionStock>();
            Transaction.Source = db.Query<TransactionStock>("GetTransactionStock", new { Between = Convert.ToInt32(this.SelectedRange.Id) * -1 }).Convert<TransactionStock>();

            SearchCommand = new DelegateCommand(new Action(OnSearch));

            db.Close();
        }

        private void OnSearch()
        {
            Transaction.Source = StockSearchEngine.Filter(Transaction.Source, searchInput);
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
                    Thread.Sleep(5000);
                }
            });
        }

        public void Stop()
        {
            run = false;
        }
    }
}
