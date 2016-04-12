using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texaco.Database;
using Texaco.Database.SqlLoader;
using Texaco.Container;
using Texaco.Database.Petapoco;
using System.Threading;
using System.Reflection;
using System.Windows;
using Koala.ViewModels.Configuration.Client;
using Koala.ViewModels.Order;
using Koala.ViewModels.Master;
using System.Windows.Markup;
using System.Globalization;
using Koala.ViewModels.Report;

namespace Koala.Core
{
    public class ApplicationStartup  
    {
        private IDbManager dbManager;
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
            //MasterCollaborator master = new MasterCollaborator();
            //DashboardCollaborator report = new DashboardCollaborator();

            ObjectPool.Instance.Register<OrderCollaborator>().ImplementedBy(order);
            //ObjectPool.Instance.Register<MasterCollaborator>().ImplementedBy(master);
            //ObjectPool.Instance.Register<DashboardCollaborator>().ImplementedBy(report);
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
                string formattedConnectionString;
                string connectionString;
                 
                formattedConnectionString = settings.Database.ConnectionStringFormat;
                connectionString = string.Format(formattedConnectionString, settings.Database.Server, settings.Database.Name, settings.Database.Username, settings.Database.Password);

                dbManager = new Texaco.Database.Petapoco.PetapocoDbManager(null, null);
                dbManager.AddSqlLoader(new ResourceSqlLoader("filesql", "Koala.SqlFiles", Assembly.GetAssembly(this.GetType())));
                dbManager.ConnectionDescriptor.Add(new ConnectionDescriptor()
                {
                    ConnectionString = connectionString,
                    IsDefault = true,
                    Name = settings.Database.Name,
                    ProviderName = settings.Database.ProviderName
                });

                ObjectPool.Instance.Register<IDbManager>().ImplementedBy(dbManager); 
            });
        }

    }
}
