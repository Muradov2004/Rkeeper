﻿using Rkeeper.Stores;
using Rkeeper.View;
using Rkeeper.ViewModel.Command;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

internal class LoginVM : BaseVM
{
    private NavigationStore _navigation;

    private string? _username;

    public string? UsernameString
    {
        get => _username;
        set
        {
            if (_username != value)
            {
                _username = value;
                NotifyPropertyChanged(nameof(UsernameString));
            }
        }
    }

    private string? _password;

    public string? PasswordString
    {
        get => _password;
        set
        {
            if (_password != value)
            {
                _password = value;
                NotifyPropertyChanged(nameof(PasswordString));
            }
        }
    }

    public ICommand? NavigateRegisterCommand { get; set; }
    public ICommand? LoginCommand { get; set; }

    public LoginVM(NavigationStore navigation)
    {
        _navigation = navigation;
        NavigateRegisterCommand = new RelayCommand(ExecuteNavigateRegisterCommand);
        LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
    }

    private bool CanExecuteLoginCommand(object? obj) => !(string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password));

    private void ExecuteLoginCommand(object? obj)
    {
        if (UsernameString == "user" && PasswordString == "user")
        {

            string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"Resources\LogFile.txt";
            File.AppendAllText(path, $"Log : {UsernameString} logged in Time : {DateTime.Now.ToString("G")}\n");
            NavigationStore _navigationStore = new();
            _navigationStore.CurrentVM = new TableVM(_navigationStore);
            MainView mainView = new MainView()
            {
                DataContext = new MainVM(_navigationStore)
            };
            mainView.Show();
            var window = Window.GetWindow(obj as DependencyObject);

            window?.Close();
        }
        else if (UsernameString == "admin" && PasswordString == "admin")
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
            var window = Window.GetWindow(obj as DependencyObject);

            window?.Close();
        }
    }

    private void ExecuteNavigateRegisterCommand(object? obj)
    {
        _navigation.CurrentVM = new RegisterVM(_navigation);
    }
}