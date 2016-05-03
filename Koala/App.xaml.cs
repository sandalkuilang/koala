using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Koala.Core;
using Koala.Views;
using Krokot.Database;
using Koala.ViewModels.User;
using Koala.ViewModels.Configuration.Client;
using Koala.ViewModels.ValidationRules;
using Koala.Core.Credential.Custom;

namespace Koala
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private int _attempts;
        private string _userName = "";
        private string _pass = "";
        public App()
        { 
            
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new ApplicationStartup();
            bootstrapper.Run();
            ShowLogOn();
        }
        private void ShowLogOn()
        {
            var logon = new LogOn(_userName, _pass);
            logon.Attempts = _attempts;
            bool? res = logon.ShowDialog();
            if (!res ?? true)
            {
                Shutdown(1);
            }
            else
            {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
                List<UserModel> result = db.Query<UserModel>("GetUser", new { Username = logon.UserName, Password = logon.Password });
                if (result.Any())
                {
                    string[] accessLevel; 
                    switch (result[0].Type)
                    {
                        case 100:
                            accessLevel = new string[] {
                                AccessLevel.CAN_VIEW_ORDER 
                            }; 
                            break;
                        case 900:
                            accessLevel = new string[] {
                                AccessLevel.CAN_VIEW_ORDER,
                                AccessLevel.CAN_VIEW_MASTER,
                                AccessLevel.CAN_VIEW_REPORT
                            }; 
                            break;
                        default:
                            accessLevel = new string[] {
                            };
                            break;
                    }
                    db.Close();
                    AuthorizationProvider.Initialize<DefaultAuthorizationProvider>(accessLevel);
                    Current.MainWindow = new MainWindow();
                    Current.MainWindow.Show();
                }
                else
                {
                    if (logon.Attempts > 2)
                    {
                        MessageBox.Show("Application is exiting due to invalid credentials", "Application Exit", MessageBoxButton.OK, MessageBoxImage.Error);
                        Shutdown(1);
                    }
                    else
                    {
                        _attempts += 1;
                        _userName = logon.UserName;
                        _pass = logon.Password;
                        ShowLogOn();
                    }
                } 
            }
        }
    }
}
