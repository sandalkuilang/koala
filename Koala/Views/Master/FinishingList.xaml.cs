using Koala.Core;
using Koala.ViewModels.Master; 
using System.Windows.Controls; 

namespace Koala.Views.Master
{
    /// <summary>
    /// Interaction logic for Finishing.xaml
    /// </summary>
    public partial class FinishingList : UserControl
    {
        public FinishingList()
        {
            InitializeComponent();
            MasterCollaborator model = ObjectPool.Instance.Resolve<MasterCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.Finishing;
            } 
        }
    }
}
