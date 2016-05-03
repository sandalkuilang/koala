using Koala.Core;
using Koala.ViewModels.Master;
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

namespace Koala.Views.Master
{
    /// <summary>
    /// Interaction logic for Supplier.xaml
    /// </summary>
    public partial class SupplierList : UserControl
    {
        public SupplierList()
        {
            InitializeComponent();
            InitializeComponent();
            MasterCollaborator model = ObjectPool.Instance.Resolve<MasterCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.Supplier;
            }
        }
    }
}
