using Rkeeper.Stores;
using Rkeeper.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Rkeeper.View.LoginRegisterView
{
    /// <summary>
    /// Interaction logic for LoginRegisterWindow.xaml
    /// </summary>
    public partial class LoginRegisterWindow : Window
    {
        private RegisterPage registerPage = new();
        private LoginPage loginPage = new();

        public LoginRegisterWindow()
        {
            InitializeComponent();
            MainContent.NavigationService.Navigate(loginPage);
            loginPage.AdditionalRegisterBtn.Click += RegisterPageBtnClick;
            registerPage.AdditionalLoginBtn.Click += LoginPageBtnClick;
            loginPage.LoginBtn.Click += Button_Click;
        }

        private void LoginPageBtnClick(object sender, RoutedEventArgs e)
        {
            MainContent.NavigationService.Navigate(loginPage);
        }

        private void RegisterPageBtnClick(object sender, RoutedEventArgs e)
        {
            MainContent.NavigationService.Navigate(registerPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loginPage.Username.Text == "user" && loginPage.Password.Password == "user")
            {

                string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"Resources\LogFile.txt";
                File.AppendAllText(path, $"Log : User logged in Time : {DateTime.Now.ToString("G")}\n");
                NavigationStore _navigationStore = new();
                _navigationStore.CurrentVM = new TableVM(_navigationStore);
                MainView mainView = new MainView()
                {
                    DataContext = new MainVM(_navigationStore)
                };
                mainView.Show();
                Close();
            }
            else if (loginPage.Username.Text == "admin" && loginPage.Password.Password == "admin")
            {
                string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"Resources\LogFile.txt";
                File.AppendAllText(path, $"Log : Admin logged in Time : {DateTime.Now.ToString("G")}\n");
                NavigationStore _navigationStore = new();
                _navigationStore.CurrentVM = new AdminVM(_navigationStore);
                MainView mainView = new MainView()
                {
                    DataContext = new MainVM(_navigationStore)
                };
                mainView.Show();
                Close();
            }
        }
    }
}
