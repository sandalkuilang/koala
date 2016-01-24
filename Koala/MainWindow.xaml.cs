using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Koala.Core;
using Koala.Navigation;
using Koala.ViewModels.Configuration.Client;

namespace Koala
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _shutdown;
        private bool firstLoaded = false;
        private System.Collections.Generic.Stack<string> history;
        public MainWindow()
        {
            InitializeComponent();
            InitializeMenu();
            history = new Stack<string>();
            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            settings.ConfigurationLoaded += settings_ConfigurationLoaded;
            if (!firstLoaded)
            {
                firstLoaded = true;


                if (settings.General.LaunchMinimized)
                    this.WindowState = WindowState.Minimized;

                if (settings.General.ShowTaskbarIcon)
                    this.ShowInTaskbar = true;
                else
                    this.ShowInTaskbar = false;

                ObjectPool.Instance.Register<Window>().ImplementedBy(this);
            }
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme("BaseLight");
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
        }

        private void settings_ConfigurationLoaded(object sender, EventArgs e)
        {

        }

        private void InitializeMenu()
        {
            HomeNavigation selector = ViewLocator.Instance.Get<HomeNavigation>();
            if (selector == null)
            {
                selector = new HomeNavigation();
                selector.Templates.Add("Home", this.Resources["Home"]);
                selector.Templates.Add("About", this.Resources["About"]);
                selector.Templates.Add("Settings", this.Resources["Settings"]);
                ViewLocator.Instance.Add(typeof(HomeNavigation), selector);
                ViewLocator.Instance.Add("TransitionContentHome", this.transitionContentHome);
            }
            this.transitionContentHome.ContentTemplateSelector = selector;
        }


        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            HomeNavigation selector = ViewLocator.Instance.Get<HomeNavigation>();
            this.transitionContentHome.Content = selector.Templates["Settings"];

            if (history.FirstOrDefault() != "about")
                this.transitionContentHome.AbortTransition();

            history.Push("settings");
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            HomeNavigation selector = ViewLocator.Instance.Get<HomeNavigation>();
            this.transitionContentHome.Content = selector.Templates["About"];

            if (history.FirstOrDefault() != "settings")
                this.transitionContentHome.AbortTransition();

            history.Push("about");
        }

        private async void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            e.Cancel = !_shutdown;
            if (_shutdown) return;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
                AnimateShow = true,
                AnimateHide = false
            };

            var result = await this.ShowMessageAsync("",
                "Are you sure want to quit application?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            _shutdown = result == MessageDialogResult.Affirmative;

            if (_shutdown)
                Application.Current.Shutdown();
        }
    }
}
