using Rkeeper.Model;
using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System.Windows;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

internal class RegisterVM : BaseVM
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

    public ICommand? NavigateLoginCommand { get; set; }
    public ICommand? RegisterCommand { get; set; }

    public RegisterVM(NavigationStore navigation)
    {
        _navigation = navigation;
        NavigateLoginCommand = new RelayCommand(ExecuteNavigateRegisterCommand);
        RegisterCommand = new RelayCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
    }
    private bool CanExecuteRegisterCommand(object? obj) => !(string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password));

    private void ExecuteRegisterCommand(object? obj)
    {
        bool condition = false;
        foreach (var item in users.users)
            if (UsernameString == item.Username)
            {
                condition = true;
                break;
            }

        if (condition)
            MessageBox.Show("Username exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        else
        {
            MessageBox.Show("Succesfully registered", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            users.AddUser(new(UsernameString, PasswordString));
        }
    }

    private void ExecuteNavigateRegisterCommand(object? obj) => _navigation.CurrentVM = new LoginVM(_navigation);
}
