using System.Windows;

namespace Rkeeper.View.LoginRegisterView
{
    /// <summary>
    /// Interaction logic for LoginRegisterWindow.xaml
    /// </summary>
    public partial class LoginRegisterWindow : Window
    {
        public LoginRegisterWindow() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e) => Close();
    }
}
