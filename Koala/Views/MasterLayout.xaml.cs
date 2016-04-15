using Koala.Core;
using Koala.ViewModels.Master;
using System.Windows.Controls;

namespace Koala.Views
{
    /// <summary>
    /// Interaction logic for Master.xaml
    /// </summary>
    public partial class MasterLayout : UserControl
    {
        private MasterCollaborator model;
        public MasterLayout()
        {
            InitializeComponent();
            model = ObjectPool.Instance.Resolve<MasterCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            } 
        }
    }
}
