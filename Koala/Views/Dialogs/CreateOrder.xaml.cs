using System.Windows; 

namespace Koala.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder 
    {
        public CreateOrder()
        {
            InitializeComponent();
        }
         
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
