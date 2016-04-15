using System.Windows;

namespace Koala.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for Size.xaml
    /// </summary>
    public partial class Size
    {
        public Size()
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
    }
}
