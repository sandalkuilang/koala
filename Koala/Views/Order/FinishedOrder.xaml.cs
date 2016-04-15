using Koala.Core;
using Koala.ViewModels.Order; 
using System.Windows.Controls; 

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
