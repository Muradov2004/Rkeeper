using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rkeeper.View.LoginRegisterView
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Password_PreviewKeyDown(object sender, KeyEventArgs e)
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
