﻿using System;
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
            if (loginPage.Username.Text == "admin" && loginPage.Password.Password == "admin")
            {
                MainView mainView = new MainView();
                mainView.Show();
                Close();
            }
        }
    }
}
