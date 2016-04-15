using Koala.Core;
using Koala.ViewModels.Master; 

namespace Koala.Views.Master
{
    /// <summary>
    /// Interaction logic for Size.xaml
    /// </summary>
    public partial class SizeList  
    {
        public SizeList()
        {
            InitializeComponent();
            MasterCollaborator model = ObjectPool.Instance.Resolve<MasterCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.Size;
            } 
        }
    }
}
