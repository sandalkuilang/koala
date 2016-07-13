using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Windows;
using Koala.ViewModels.Configuration.Client;
using Koala.ViewModels.Order;
using Koala.ViewModels.Master;
using System.Windows.Markup;
using System.Globalization;
using Koala.ViewModels.Report;
using Krokot.Database;
using Krokot.Database.SqlLoader;
using Koala.Core.Credential;
using Koala.ViewModels.Stock;

namespace Koala.Core
{
    public class ApplicationStartup  
    { 
        private ApplicationSettings settings;
         

        public void Run()
        {
           
            InitializeConfiguration();
            InitializeDatabase();
            InitializeDialog();
            InitializeModels();
            try
            {
                CultureInfo ci = new System.Globalization.CultureInfo(settings.General.Culture);
                ci.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";

                Thread.CurrentThread.CurrentCulture = ci;
                FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            }
            catch { } 

        }
         
        private void InitializeModels()
        {
            OrderCollaborator order = new OrderCollaborator();
            MasterCollaborator master = new MasterCollaborator();
            DashboardCollaborator report = new DashboardCollaborator();
            StockCollaborator stock = new StockCollaborator();

            ObjectPool.Instance.Register<OrderCollaborator>().ImplementedBy(order);
            ObjectPool.Instance.Register<MasterCollaborator>().ImplementedBy(master);
            ObjectPool.Instance.Register<DashboardCollaborator>().ImplementedBy(report);
            ObjectPool.Instance.Register<StockCollaborator>().ImplementedBy(stock);
        }

        private void InitializeAudio()
        {
            IAudioService audio = new DefaultAudioService();

            if (!string.IsNullOrEmpty(settings.Audio.Error))
            {
                audio.Register(new Audio()
                {
                    Url = string.Join("", System.AppDomain.CurrentDomain.BaseDirectory, settings.Audio.Error),
                    Type = AudioEnum.Error
                });
            }
            if (!string.IsNullOrEmpty(settings.Audio.Warning))
            {
                audio.Register(new Audio()
                {
                    Url = string.Join("", System.AppDomain.CurrentDomain.BaseDirectory, settings.Audio.Warning),
                    Type = AudioEnum.Warning
                });
            }

            ObjectPool.Instance.Register<IAudioService>().ImplementedBy(audio);
        }

        private void InitializeDialog()
        {
            IDialogService dialog = new DialogService();

            dialog.Register(new Views.Dialogs.CreateOrder());
            dialog.Register(new Views.Dialogs.CreateOrderDetail());
            dialog.Register(new Views.Dialogs.Finishing());
            dialog.Register(new Views.Dialogs.Material());
            dialog.Register(new Views.Dialogs.PrintQuality());
            dialog.Register(new Views.Dialogs.Size());
            dialog.Register(new Views.Dialogs.Warning());
            dialog.Register(new Views.Dialogs.PrintInvoice());
            dialog.Register(new Views.Dialogs.YesNo());
            dialog.Register(new Views.Dialogs.Supplier());
            dialog.Register(new Views.Dialogs.Stock());

            ObjectPool.Instance.Register<IDialogService>().ImplementedBy(dialog);
        }

        private void InitializeConfiguration()
        {
            settings = new ApplicationSettings();
            string photoshop = RegistryFinder.Find(Microsoft.Win32.Registry.ClassesRoot, "CLSID", "Adobe Photoshop");
            settings.OpenWith.Photoshop = photoshop;

            ObjectPool.Instance.Register<ApplicationSettings>().ImplementedBy(settings); 
        }

        private void InitializeDatabase()
        {
            Task.Factory.StartNew(() =>
            { 
                IEncryptionAgent encryption;
                string passwd;

                IDbManager dbManager = new Krokot.Database.Petapoco.PetapocoDbManager(null, null);
                IFileLoader resourceloader = new ResourceSqlLoader("resource-loader", "SqlFiles", Assembly.GetAssembly(this.GetType()));
                dbManager.AddSqlLoader(resourceloader);
 
                ConnectionStringCollection connections = ApplicationSettings.Instance.Database.Items;
                foreach (ConnectionStringElement connection in connections)
                {
                    encryption = new RijndaelEncryption(connection.Key, connection.IV);
                    passwd = encryption.Decrypt(connection.Password);

                    dbManager.ConnectionDescriptor.Add(new ConnectionDescriptor()
                    {
                        ConnectionString = string.Format(connection.ConnectionString, connection.UserId, passwd),
                        IsDefault = connection.IsDefault,
                        Name = connection.Name,
                        ProviderName = connection.ProviderName
                    });
                }

                ObjectPool.Instance.Register<IDbManager>().ImplementedBy(dbManager); 
            });
        }

    }
}
