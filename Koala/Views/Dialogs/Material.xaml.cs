using System.Windows;

namespace Koala.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for Material.xaml
    /// </summary>
    public partial class Material
    {
        public Material()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
         
    }
}
