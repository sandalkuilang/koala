using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Master
{
    public class BaseMasterModel : BaseGridRow
    { 
        private string id;
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                NotifyIfChanged(ref id, value);
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                NotifyIfChanged(ref description, value);
            }
        }
    }
}
