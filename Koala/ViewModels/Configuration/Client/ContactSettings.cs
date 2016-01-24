using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Configuration.Client
{
    public class ContactSettings : BaseConfigurationSection
    {

        private string company;
        [ConfigurationProperty("company", DefaultValue = "")]
        public string Company
        {
            get
            {
                company = (string)this["company"];
                return company;
            }
            set
            {
                NotifyIfChanged(ref company, value);
            }
        }

        private string address;
        [ConfigurationProperty("address", DefaultValue = "")]
        public string Address
        {
            get
            {
                address = (string)this["address"];
                return address;
            }
            set
            {
                NotifyIfChanged(ref address, value);
            }
        }

        private string phone;
        [ConfigurationProperty("phone", DefaultValue = "")]
        public string Phone
        {
            get
            {
                phone = (string)this["phone"];
                return phone;
            }
            set
            {
                NotifyIfChanged(ref phone, value);
            }
        }

        private string email;
        [ConfigurationProperty("email", DefaultValue = "")]
        public string Email
        {
            get
            {
                email = (string)this["email"];
                return email;
            }
            set
            {
                NotifyIfChanged(ref email, value);
            }
        }

    }
}
