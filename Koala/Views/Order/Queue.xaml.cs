using Koala.Core;
using Koala.ViewModels.Order; 
using System.Windows.Controls; 

namespace Koala.Views.Order
{
    /// <summary>
    /// Interaction logic for Queue.xaml
    /// </summary>
    public partial class Queue : UserControl
    {
        private OrderCollaborator model;
        public Queue()
        {
            InitializeComponent();
            model = ObjectPool.Instance.Resolve<OrderCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.Queue;
            } 
        }
    }
}
