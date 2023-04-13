using System.Windows;
using System.Windows.Controls;

namespace Rkeeper.View.LoginRegisterView
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TextBlock placeHolderTextBlock = (TextBlock)Password.Template.FindName("PlaceHolder", Password);
            if (string.IsNullOrEmpty(Password.Password))
            {
                placeHolderTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                placeHolderTextBlock.Visibility = Visibility.Collapsed;
            }
        }
    }
}
