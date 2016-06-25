using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Koala.ViewModels.Stock
{
    public class TransactionStock : BaseGridRow, ISaveCommand
    {
        public int TransId { get; set; }
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName{ get; set; }
        public string QualityId { get; set; }
        public string QualityName { get; set; }
        public int Qty { get; set; }
        public string StatusName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public ICommand AddCommand
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
