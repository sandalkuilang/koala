using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Koala.ViewModels.Configuration.Client
{
    public class DatabaseSettings : BaseConfigurationSection
    {

        private string connectionStringFormat;
        [ConfigurationProperty("connectionStringFormat")]
        public string ConnectionStringFormat
        {
            get
            {
                server = (string)this["connectionStringFormat"];
                return server;
            }
            set
            {
                NotifyIfChanged(ref connectionStringFormat, value);
            }
        }

        private string server;
        [ConfigurationProperty("server")]
        public string Server
        {
            get
            {
                server = (string)this["server"];
                return server;
            }
            set
            {
                NotifyIfChanged(ref server, value);
            }
        }

        private string name;
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                name = (string)this["name"];
                return name;
            }
            set
            {
                NotifyIfChanged(ref name, value);
            }
        }

        private string userName;
        [ConfigurationProperty("userName")]
        public string Username
        {
            get
            {
                userName = (string)this["userName"];
                return userName;
            }
            set
            {
                NotifyIfChanged(ref userName, value);
            }
        }

        private string password;
        [ConfigurationProperty("password")]
        public string Password
        {
            get
            {
                password = (string)this["password"];
                return password;
            }
            set
            {
                NotifyIfChanged(ref password, value);
            }
        }

        private string providerName;
        [ConfigurationProperty("providerName")]
        public string ProviderName
        {
            get
            {
                providerName = (string)this["providerName"];
                return providerName;
            }
            set
            {
                NotifyIfChanged(ref providerName, value);
            }
        }

    }
}
