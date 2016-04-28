using System.Configuration;

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
                this["company"] = value;
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
                this["address"] = value;
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
                this["phone"] = value;
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
                this["email"] = value;
                NotifyIfChanged(ref email, value);
            }
        }

    }
}
