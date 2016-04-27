using System;
using ReactiveUI;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Koala.ViewModels.Report
{
    public class DashboardCollaborator : BaseBindableModel, ISynchronize
    {
        
        public ReactiveCommand<Object> RefreshCommand { get; set; }
     
        private InvoiceMonthViewSource invoiceMonth;
        public InvoiceMonthViewSource InvoiceMonth
        {
            get
            {
                return invoiceMonth;
            }
            set
            {
                NotifyIfChanged(ref invoiceMonth, value);
            }
        }

        private RemainingPerMonthViewSource remainingPerMonth;
        public RemainingPerMonthViewSource RemainingPerMonth
        {
            get
            {
                return remainingPerMonth;
            }
            set
            {
                NotifyIfChanged(ref remainingPerMonth, value);
            }
        }

        private TopMaterialViewSource topMaterial;
        public TopMaterialViewSource TopMaterial
        {
            get
            {
                return topMaterial;
            }
            set
            {
                NotifyIfChanged(ref topMaterial, value);
            }
        }

        private TopConsumerViewSource topConsumer;
        public TopConsumerViewSource TopConsumer
        {
            get
            {
                return topConsumer;
            }
            set
            {
                NotifyIfChanged(ref topConsumer, value);
            }
        }
 
        public DashboardCollaborator()
        {
            InvoiceMonth = new InvoiceMonthViewSource();
            TopMaterial = new TopMaterialViewSource();
            TopConsumer = new TopConsumerViewSource();
            RemainingPerMonth = new RemainingPerMonthViewSource();

            RefreshCommand = ReactiveCommand.Create();
            RefreshCommand.Subscribe(x => 
            {
                OnRefresh(x);
            });

            RefreshCommand.Execute(null);
        }

        private void OnRefresh(object parameter)
        {
            Task.Run(() =>
            {
                if (parameter != null)
                {
                    switch (parameter.ToString())
                    {
                        case "1":
                            invoiceMonth.Load();
                            break;
                        case "2":   
                            remainingPerMonth.Load();
                            break;
                        case "3":
                            topConsumer.Load();
                            break;
                        case "4":
                            topMaterial.Load();
                            break;
                    }
                }
                else
                {
                    invoiceMonth.Load();
                    topMaterial.Load();
                    topConsumer.Load();
                    remainingPerMonth.Load();
                }
            });
        }

        public void Pull()
        {
            RefreshCommand.Execute(null);
        }

        public void Push()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
