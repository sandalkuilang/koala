using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Koala.Core;
using Koala.Navigation;
using Koala.ViewModels.Configuration.Client;
using MahApps.Metro.Controls.Dialogs;
using Koala.ViewModels.ValidationRules;
using Koala.Core.Credential;
using Krokot.Database;
using Koala.ViewModels.User;
using Koala.ViewModels.Order;
using Koala.ViewModels.Master;
using Koala.ViewModels.Report;

namespace Koala
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        internal static MainWindow Current { get; private set; }

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
            if (theme != null)
                ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
            else
            {
                var accent = ThemeManager.Accents.First(x => x.Name == "Blue");
                ThemeManager.ChangeAppStyle(Application.Current, accent, appTheme);
            }
                

            Current = this; 
        }

        private void settings_ConfigurationLoaded(object sender, EventArgs e)
        { 

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            //if (AuthorizationProvider.Instance == null)
            //    MainWindow.Current.ShowLogin();
        }

        public async void ShowLogin()
        {
            LoginDialogData x = await this.ShowLoginAsync("Login", "Enter your credentials", new LoginDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Accented,
                EnablePasswordPreview = true,
                PasswordWatermark = "password",
                UsernameWatermark = "user name",
                InitialPassword = "",
                InitialUsername = "",
                DefaultButtonFocus = MessageDialogResult.Affirmative,
                AnimateShow = true
            });
            
            if ((x == null))
            {
                ShowLogin();
            }
            else
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
                List<UserModel> result = db.Query<UserModel>("GetUser", new { Username = x.Username, Password = x.Password });
                if (result.Any())
                {
                    //string[] accessLevel; 
                    switch (result[0].Type)
                    {
                        case 100:
                            ObjectPool.Instance.Register<MasterCollaborator>().ImplementedBy(new MasterCollaborator());
 
                            break;
                        case 900:
                            ObjectPool.Instance.Register<MasterCollaborator>().ImplementedBy(new MasterCollaborator());
                            ObjectPool.Instance.Register<DashboardCollaborator>().ImplementedBy(new DashboardCollaborator());
                            break; 
                    } 
                    //AuthorizationProvider.Initialize<DefaultAuthorizationProvider>(accessLevel);
                    db.Close();
                }
                else
                {

                    db.Close();
                    ShowLogin();
                }
            }
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
