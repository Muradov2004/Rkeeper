using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

internal class RegisterVM : BaseVM
{
    private NavigationStore _navigation;
    
    public ICommand? NavigateLoginCommand { get; set; }
    
    public RegisterVM(NavigationStore navigation)
    {
        _navigation = navigation;
        NavigateLoginCommand = new RelayCommand(ExecuteNavigateRegisterCommand);
    }

    private void ExecuteNavigateRegisterCommand(object? obj)
    {
        _navigation.CurrentVM = new LoginVM(_navigation);
    }
}
