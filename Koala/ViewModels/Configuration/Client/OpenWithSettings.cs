using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Configuration.Client
{
    public class OpenWithSettings : BaseBindableModel
    {
        private string photoshop; 
        public string Photoshop
        {
            get
            { 
                return photoshop;
            }
            set
            {
                NotifyIfChanged(ref photoshop, value);
            }
        }

        private string autocad; 
        public string Autocad
        {
            get
            { 
                return photoshop;
            }
            set
            {
                NotifyIfChanged(ref autocad, value);
            }
        }

    }
}
