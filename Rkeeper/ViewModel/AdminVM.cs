using Rkeeper.Stores;
using Rkeeper.View.LoginRegisterView;
using Rkeeper.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

internal class AdminVM : BaseVM
{
    private readonly NavigationStore _navigation;

    public ICommand? AddFoodToMenuCommand { get; set; }
    public ICommand? TableCommand { get; set; }
    public ICommand? LogFileCommand { get; set; }
    public ICommand? LogoutCommand { get; set; }
    
    public AdminVM(NavigationStore navigation)
    {
        _navigation = navigation;
        AddFoodToMenuCommand = new RelayCommand(ExecuteAddFoodToMenuCommand);
        TableCommand = new RelayCommand(ExecuteTableCommand);
        LogFileCommand = new RelayCommand(ExecuteLogFileCommand);
        LogoutCommand = new RelayCommand(ExecuteLogoutCommand);
    }

    private void ExecuteAddFoodToMenuCommand(object? obj)
    {
        _navigation.CurrentVM = new MenuFoodVM(_navigation);
    }
    private void ExecuteTableCommand(object? obj)
    {
        _navigation.CurrentVM = new AdminTableVM(_navigation);
    }

    private void ExecuteLogFileCommand(object? obj)
    {
        _navigation.CurrentVM = new LogfileVM(_navigation);
    }

    private void ExecuteLogoutCommand(object? obj)
    {

        LoginRegisterWindow loginRegisterWindow = new LoginRegisterWindow();
        loginRegisterWindow.Show();
        
        var window = Window.GetWindow(obj as DependencyObject);

        window?.Close();
    }


}
