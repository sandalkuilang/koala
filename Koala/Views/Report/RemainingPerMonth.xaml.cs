using Koala.Core;
using Koala.ViewModels.Report;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Koala.Views.Report
{
    /// <summary>
    /// Interaction logic for PerMonth.xaml
    /// </summary>
    public partial class RemainingPerMonth : UserControl
    {
        public RemainingPerMonth()
        {
            InitializeComponent(); 
        } 

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Task.Run(() => {
                Dispatcher.Invoke(() => {
                    DashboardCollaborator model = ObjectPool.Instance.Resolve<DashboardCollaborator>();
                    if (this.DataContext == null && model != null)
                    {
                        this.DataContext = model;
                    }
                });
            });
        }

    }
}
