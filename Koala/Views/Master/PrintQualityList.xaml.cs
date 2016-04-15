using Koala.Core;
using Koala.ViewModels.Master; 
using System.Windows.Controls; 

namespace Koala.Views.Master
{
    /// <summary>
    /// Interaction logic for PrintQuality.xaml
    /// </summary>
    public partial class PrintQualityList : UserControl
    {
        public PrintQualityList()
        {
            InitializeComponent();
            MasterCollaborator model = ObjectPool.Instance.Resolve<MasterCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model.Quality;
            } 
        }
    }
}
