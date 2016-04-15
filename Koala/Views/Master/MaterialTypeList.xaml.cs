using Koala.Core;
using Koala.ViewModels.Master; 
using System.Windows.Controls; 

namespace Koala.Views.Master
{
    /// <summary>
    /// Interaction logic for ItemType.xaml
    /// </summary>
    public partial class MaterialTypeList : UserControl
    {
        public MaterialTypeList()
        {
            InitializeComponent();
            MasterCollaborator model = ObjectPool.Instance.Resolve<MasterCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.Material;
            } 
        }
    }
}
