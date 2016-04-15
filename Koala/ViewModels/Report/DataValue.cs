using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Report
{
    public class DataValue : BaseBindableModel
    {
        private string name;
        public string Name
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
