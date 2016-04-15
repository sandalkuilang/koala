using Koala.Core;
using Koala.ViewModels.Order;
using System.Windows.Controls;

namespace Koala.Views
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class OrderLayout : UserControl
    {
        private OrderCollaborator model;
        public OrderLayout()
        {
            InitializeComponent();
            model = ObjectPool.Instance.Resolve<OrderCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            } 
        }
    }
}
