using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Report
{
    public class InvoiceDataValue : DataValue
    { 
        private int orderInstallment;
        public int OrderInstallment
        {
            get
            {
                return orderInstallment;
            }
            set
            {
                NotifyIfChanged(ref orderInstallment, value);
            }
        }

        private decimal totalInstallment;
        public decimal TotalInstallment
        {
            get
            {
                return totalInstallment;
            }
            set
            {
                NotifyIfChanged(ref totalInstallment, value);
            }
        }

        private decimal total;
        public decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                NotifyIfChanged(ref total, value);
            }
        }

        private int order;
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                NotifyIfChanged(ref order, value);
            }
        }
    }
}
