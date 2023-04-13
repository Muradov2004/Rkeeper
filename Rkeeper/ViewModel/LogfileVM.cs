using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

internal class LogfileVM : BaseVM
{
    private readonly NavigationStore _navigation;

    public string LogFileString { get; set; }

    public ICommand? BackCommand { get; set; }

    public LogfileVM(NavigationStore navigation)
    {

        _navigation = navigation;
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"Resources\LogFile.txt";
        LogFileString = File.ReadAllText(path);
        BackCommand = new RelayCommand(ExecuteBackCommand);

    }

    private void ExecuteBackCommand(object? obj)
    {
        _navigation.CurrentVM = new AdminVM(_navigation);
    }
}
