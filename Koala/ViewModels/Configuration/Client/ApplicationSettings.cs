using Koala.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Configuration.Client
{
    public class ApplicationSettings
    {

        public static ApplicationSettings Instance
        {
            get
            {
                return ObjectPool.Instance.Resolve<ApplicationSettings>();
            }
        }

        public ContactSettings Contact { get; set; }
        public GeneralSettings General { get; set; }
        public DatabaseSettings Database { get; set; }
        public AudioSettings Audio { get; set; }
        public ConfigurationCommand Command { get; set; }
        public OpenWithSettings OpenWith { get; set; }
        public System.Configuration.Configuration Configuration { get; private set; }

        public event EventHandler ConfigurationLoaded;
        public ApplicationSettings()
        {
            Command = new ConfigurationCommand();
            Command.SaveCommand = new DelegateCommand(new Action(Save));
            Command.RouteConnectionCommand = new DelegateCommand(new Action(RouteConnection));
            Command.ButtonContent = "Connect";
            Command.IsEnabled = true;

            Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            General = (GeneralSettings)Configuration.Sections["generalSettings"];
            Contact = (ContactSettings)Configuration.Sections["contactSettings"];
            Database = (DatabaseSettings)Configuration.Sections["databaseSettings"];
            Audio = (AudioSettings)Configuration.Sections["audioSettings"];
            OpenWith = new OpenWithSettings();
        }

        public void Save()
        {

        }

        public void ContactConnect()
        {

        }

        public void RouteConnection()
        {

        }

        public void Disconnect()
        {

        }

        internal bool LoadServerConfiguration()
        {

            OnConfigurationLoaded();

            return true;
        }


        private void OnConfigurationLoaded()
        {
            if (ConfigurationLoaded != null)
                ConfigurationLoaded(this, null);
        }
    }
}
