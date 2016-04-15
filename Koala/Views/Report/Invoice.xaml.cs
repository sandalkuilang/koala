using Koala.Core;
using Koala.ViewModels.Report;
using System.Threading.Tasks;
using System.Windows.Controls;
using System;

namespace Koala.Views.Report
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Invoice : UserControl
    {
        public Invoice()
        {
            InitializeComponent();
            Dispatcher.InvokeAsync(() => {
                DashboardCollaborator model = ObjectPool.Instance.Resolve<DashboardCollaborator>();
                if (this.DataContext == null && model != null)
                {
                    this.DataContext = model;
                }
            });
        }
         
    }
}
