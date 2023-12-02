using Rkeeper.Model;
using Rkeeper.Stores;
using Rkeeper.View;
using Rkeeper.View.OpeningLoadingView;
using Rkeeper.ViewModel.Command;
using System;
using System.IO;
using System.Threading.Tasks;
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

    private Users users = new();

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
        bool condition = true;
        foreach (var item in users.users)
            if (UsernameString == item.Username)
            {
                if (PasswordString == item.Password)
                {

                    Loding loadingWindow = new();
                    loadingWindow.ShowDialog();
                    string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"Resources\LogFile.txt";
                    NavigationStore _navigationStore = new();
                    if (UsernameString == "admin")
                    {
                        File.AppendAllText(path, $"Log : Admin logged in Time : {DateTime.Now.ToString("G")}\n");
                        _navigationStore.CurrentVM = new AdminVM(_navigationStore);
                        MainView mainView = new MainView()
                        {
                            DataContext = new MainVM(_navigationStore)
                        };
                        mainView.Show();
                    }
                    else
                    {

                        File.AppendAllText(path, $"Log : {UsernameString} logged in Time : {DateTime.Now.ToString("G")}\n");
                        _navigationStore.CurrentVM = new TableVM(_navigationStore) { Username = UsernameString! };
                        MainView mainView = new MainView()
                        {
                            DataContext = new MainVM(_navigationStore)
                        };
                        mainView.Show();

                    }
                    var window = Window.GetWindow(obj as DependencyObject);

                    window?.Close();
                    condition = false;
                    break;
                }

            }

        if (condition)
            MessageBox.Show("Username or Password is wrong", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        UsernameString = "";
        PasswordString = "";

    }

    private void ExecuteNavigateRegisterCommand(object? obj) => _navigation.CurrentVM = new RegisterVM(_navigation);
}
