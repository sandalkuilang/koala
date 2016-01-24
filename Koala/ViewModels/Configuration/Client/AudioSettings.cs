using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Configuration.Client
{
    public class AudioSettings : BaseConfigurationSection
    {
        
        [ConfigurationProperty("error")]
        public string Error
        {
            get
            {
                return (string)this["error"];
            }
            set
            {
                this["error"] = value;
                OnPropertyChanged("error");
            }
        }

        [ConfigurationProperty("warning")]
        public string Warning
        {
            get
            {
                return (string)this["warning"];
            }
            set
            {
                this["warning"] = value;
                OnPropertyChanged("warning");
            }
        } 
    }
}
