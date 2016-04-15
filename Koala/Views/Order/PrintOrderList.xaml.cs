using Koala.Core;
using Koala.ViewModels.Order; 
using System.Windows.Controls; 

namespace Koala.Views.Order
{
    /// <summary>
    /// Interaction logic for Issued.xaml
    /// </summary>
    public partial class PrintOrderList : UserControl
    {
        private OrderCollaborator model;
        public PrintOrderList()
        {
            InitializeComponent();
            model = ObjectPool.Instance.Resolve<OrderCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.PrintOrder;
            } 
        }
    }
}
