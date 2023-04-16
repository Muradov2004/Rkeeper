using Rkeeper.Model;
using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

class BillVM : BaseVM
{
    public ObservableCollection<Food> OrderedFood { get; set; } = new();

    public string? TableName;

    private readonly NavigationStore _navigation;

    public ICommand? CloseTableCommand { get; set; }
    public ICommand? BackCommand { get; set; }
    public BillVM(NavigationStore navigation)
    {
        _navigation = navigation;
        CloseTableCommand = new RelayCommand(ExecuteCloseTableCommand, CanExecuteCloseTableCommand);
        BackCommand = new RelayCommand(ExecuteBackCommand);
    }

    private bool CanExecuteCloseTableCommand(object? obj)
    {
        return OrderedFood.Last().Price > 0;
    }

    private void ExecuteCloseTableCommand(object? obj)
    {
        TableCollection tableCollection = new();
        tableCollection.Tables.FirstOrDefault(t => t.Name == TableName).OrderedFood.Clear();
        tableCollection.TablesToJson();
        _navigation.CurrentVM = new TableVM(_navigation);
    }
    private void ExecuteBackCommand(object? obj)
    {
        _navigation.CurrentVM = new TableVM(_navigation);
    }
}
