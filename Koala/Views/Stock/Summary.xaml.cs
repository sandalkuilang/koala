using Koala.Core;
using Koala.ViewModels.Stock;
using System.Windows.Controls;

namespace Koala.Views.Stock
{
    /// <summary>
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : UserControl
    {
        public Summary()
        {
            InitializeComponent();
            StockCollaborator model = ObjectPool.Instance.Resolve<StockCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.Summary;
            }
        }
         
    }
}
