using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Report
{
    public class InvoiceDataValue : BaseBindableModel
    {
        private string name;
        public string Month
        {
            get
            {
                return name;
            }
            set
            {
                NotifyIfChanged(ref name, value);
            }
        }

        private decimal number;
        public decimal Number
        {
            get
            {
                return number;
            }
            set
            {
                NotifyIfChanged(ref number, value);
            }
        }

    }
}
