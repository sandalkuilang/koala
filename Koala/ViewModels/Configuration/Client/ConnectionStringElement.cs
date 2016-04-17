using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Configuration.Client
{
    public class ConnectionStringElement : ConfigurationSection
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("connectionString")]
        public string ConnectionString
        {
            get
            {
                return (string)this["connectionString"];
            }
            set
            {
                this["connectionString"] = value;
            }
        }

        [ConfigurationProperty("providerName")]
        public string ProviderName
        {
            get
            {
                return (string)this["providerName"];
            }
            set
            {
                this["providerName"] = value;
            }
        }

        [ConfigurationProperty("isDefault")]
        public bool IsDefault
        {
            get
            {
                return (bool)this["isDefault"];
            }
            set
            {
                this["isDefault"] = value;
            }
        }

        [ConfigurationProperty("key")]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }

        [ConfigurationProperty("iv")]
        public string IV
        {
            get
            {
                return (string)this["iv"];
            }
            set
            {
                this["iv"] = value;
            }
        }

        [ConfigurationProperty("userId")]
        public string UserId
        {
            get
            {
                return (string)this["userId"];
            }
            set
            {
                this["userId"] = value;
            }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
            set
            {
                this["password"] = value;
            }
        }
    }
}
