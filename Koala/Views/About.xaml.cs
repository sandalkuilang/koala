﻿using MahApps.Metro.Controls;
using Koala.Navigation;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Koala.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            //lblVersion.Content = Assembly.GetEntryAssembly().GetName().Version.ToString(3);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeNavigation home = Core.ViewLocator.Instance.Get<HomeNavigation>();
            TransitioningContentControl selector = Core.ViewLocator.Instance.Get<TransitioningContentControl>("TransitionContentHome");
            selector.Transition = TransitionType.Right;
            selector.Content = home.Templates["Home"];
            selector.Transition = TransitionType.Left;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Hyperlink thisLink = (Hyperlink)sender;
            string navigateUri = thisLink.NavigateUri.ToString();
            Process.Start(new ProcessStartInfo(navigateUri));
            e.Handled = true;
        }
    }
}
