using System.Windows;

namespace Koala.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateOrder.xaml
    /// </summary>
    public partial class CreateOrderDetail
    {
        public CreateOrderDetail()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
