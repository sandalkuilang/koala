﻿using MahApps.Metro.Controls;
using Koala.ViewModels.Configuration.Client;
using Koala.Core;
using Koala.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace Koala.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            ApplicationSettings model = ObjectPool.Instance.Resolve<ApplicationSettings>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeNavigation home = ViewLocator.Instance.Get<HomeNavigation>();
            TransitioningContentControl selector = ViewLocator.Instance.Get<TransitioningContentControl>("TransitionContentHome");
            selector.Transition = TransitionType.Right;
            selector.Content = home.Templates["Home"];
            selector.Transition = TransitionType.Left;
        }

        private void ButtonRestart_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
