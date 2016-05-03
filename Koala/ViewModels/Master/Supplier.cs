using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Koala.ViewModels.Master
{
    public class Supplier : BaseGridRow
    {
        public string SupplierId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telp { get; set; }
        
        
    }
}
