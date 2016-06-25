using Koala.Core;
using Koala.ViewModels.Stock; 
using System.Windows.Controls; 

namespace Koala.Views.Stock
{
    /// <summary>
    /// Interaction logic for TransactionStock.xaml
    /// </summary>
    public partial class Transaction : UserControl
    {
        public Transaction()
        {
            InitializeComponent();
            StockCollaborator model = ObjectPool.Instance.Resolve<StockCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.Transaction;
            }
        }
    }
}
