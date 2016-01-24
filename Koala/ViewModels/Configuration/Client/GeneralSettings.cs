using Koala.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Koala.ViewModels.Configuration.Client
{
    public class GeneralSettings : BaseConfigurationSection
    {

        private string culture;
        [ConfigurationProperty("culture")]
        public string Culture
        {
            get
            {
                culture = (string)this["culture"];
                return culture;
            }
            set
            {
                this["culture"] = value;
                NotifyIfChanged(ref culture, value);
            }
        }

        private string signature;
        [ConfigurationProperty("signature")]
        public string Signature
        {
            get
            {
                signature = (string)this["signature"];
                return signature;
            }
            set
            {
                this["signature"] = value;
                NotifyIfChanged(ref signature, value);
            }
        }


        private bool desktopNotifications;
        [ConfigurationProperty("desktopNotifications")]
        public bool DesktopNotifications
        {
            get
            {
                desktopNotifications =  (bool)this["desktopNotifications"];
                return desktopNotifications;
            }
            set
            {
                this["desktopNotifications"] = value;
                NotifyIfChanged(ref desktopNotifications, value);
            }
        }

        private bool sounds;
        [ConfigurationProperty("sounds")]
        public bool Sounds
        {
            get
            {
                sounds = (bool)this["sounds"];
                return sounds;
            }
            set
            {
                this["sounds"] = value;
                NotifyIfChanged(ref sounds, value);
            }
        }

        private bool showTrayIcon;
        [ConfigurationProperty("showTrayIcon")]
        public bool ShowTrayIcon
        {
            get
            {
                showTrayIcon = (bool)this["showTrayIcon"];
                return showTrayIcon;
            }
            set
            {
                this["showTrayIcon"] = value;
                NotifyIfChanged(ref showTrayIcon, value);
            }

        }

        private bool showTaskbarIcon;
        [ConfigurationProperty("showTaskbarIcon")]
        public bool ShowTaskbarIcon
        {
            get
            {
                showTaskbarIcon = (bool)this["showTaskbarIcon"];
                return showTaskbarIcon;
            }
            set
            {
                Window mainWindow = ObjectPool.Instance.Resolve<Window>();
                if (mainWindow != null)
                    mainWindow.ShowInTaskbar = value;

                this["showTaskbarIcon"] = value;
                NotifyIfChanged(ref showTaskbarIcon, value);
            }
        }

        private bool launchWhenSystemStarts;
        [ConfigurationProperty("launchWhenSystemStarts")]
        public bool LaunchWhenSystemStarts
        {
            get
            {
                launchWhenSystemStarts =  (bool)this["launchWhenSystemStarts"];
                return launchWhenSystemStarts;
            }
            set
            {
                if (value)
                    StartUpManager.AddApplicationToCurrentUserStartup();
                else
                    StartUpManager.RemoveApplicationFromCurrentUserStartup();

                this["launchWhenSystemStarts"] = value;
                NotifyIfChanged(ref launchWhenSystemStarts, value);
            }
        }

        private bool launchMinimized;
        [ConfigurationProperty("launchMinimized", DefaultValue = "false")]
        public bool LaunchMinimized
        {
            get
            {
                launchMinimized = (bool)this["launchMinimized"];
                return launchMinimized;
            }
            set
            {
                this["launchMinimized"] = value;
                NotifyIfChanged(ref launchMinimized, value);
            }
        }
    }
}
