using Koala.Core;
using Koala.ViewModels.Stock;
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

namespace Koala.Views
{
    /// <summary>
    /// Interaction logic for Stock.xaml
    /// </summary>
    public partial class StockLayout : UserControl
    {
        private StockCollaborator model;

        public StockLayout()
        {
            InitializeComponent();
            model = ObjectPool.Instance.Resolve<StockCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            }
        }
    }
}
