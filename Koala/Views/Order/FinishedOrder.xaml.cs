using Koala.Core;
using Koala.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Koala.Views.Order
{
    /// <summary>
    /// Interaction logic for Out.xaml
    /// </summary>
    public partial class FinishedOrder : UserControl
    {
        private OrderCollaborator model;
        public FinishedOrder()
        {
            InitializeComponent();
            model = ObjectPool.Instance.Resolve<OrderCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.PaymentOrder;
            } 
        }
    }
}
