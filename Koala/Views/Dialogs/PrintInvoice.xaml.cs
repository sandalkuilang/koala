using System.Windows;

namespace Koala.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class PrintInvoice  
    {
        public PrintInvoice()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
