using Koala.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using Texaco.Database;
using Koala.ViewModels.Configuration.Client;

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

        
        public DashboardCollaborator()
        {
            InvoiceMonth = new InvoiceMonthViewSource();
            RefreshCommand = ReactiveCommand.Create();
            RefreshCommand.Subscribe(x => 
            {
                OnRefresh();
            });

            RefreshCommand.Execute(null);
        }

        private void OnRefresh()
        {  
            InvoiceMonth.Load(); 
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
