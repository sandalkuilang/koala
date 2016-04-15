using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Koala.Views
{
    /// <summary>
    /// Interaction logic for LogOn.xaml
    /// </summary>
    public partial class LogOn : MetroWindow, INotifyPropertyChanged
    {

        #region Private Fields

        private int _attempts;

        #endregion

        #region Public Properties

        public int Attempts
        {
            get { return _attempts; }
            set
            {
                if (value != _attempts)
                {
                    _attempts = value;
                    OnPropertyChanged("Attempts");
                }
            }
        }

        public Visibility ShowInvalidCredentials
        {
            get
            {
                if (_attempts > 0)
                {
                    return Visibility.Visible;
                }
                return Visibility.Hidden;
            }
        }

        public string UserName
        {
            get { return txtUsername.Text; }
        }

        public string Password
        {
            get { return txtPassword.Password; }
        }

        #endregion

        public LogOn() : this(string.Empty, string.Empty) { }

        public LogOn(string userName, string password)
        {
            InitializeComponent();

            DataContext = this;
            txtUsername.Focus();
            txtUsername.Text = userName;
            txtPassword.Password = password;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { PropertyChangedEvent += value; }
            remove { PropertyChangedEvent -= value; }
        }

        #endregion

        private void LogonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close(); 
        }

        private void CredentialsFocussed(object sender, RoutedEventArgs e)
        {
            TextBoxBase tb = sender as TextBoxBase;
            if (tb == null)
            {
                PasswordBox pwb = sender as PasswordBox;
                pwb.SelectAll();
            }
            else
            {
                tb.SelectAll();
            }
        }

        private event PropertyChangedEventHandler PropertyChangedEvent;

        protected void OnPropertyChanged(string prop)
        {
            if (PropertyChangedEvent != null)
                PropertyChangedEvent(this, new PropertyChangedEventArgs(prop));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
