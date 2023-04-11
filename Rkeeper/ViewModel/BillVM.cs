using Rkeeper.Model;
using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

class BillVM : BaseVM
{
    public ObservableCollection<Food> OrderedFood { get; set; } = new();
    public double TotalPrice { get; set; }

    private readonly NavigationStore _navigation;

    public ICommand? CloseTableCommand { get; set; }
    public ICommand? BackCommand { get; set; }
    public BillVM(NavigationStore navigation)
    {
        _navigation = navigation;
        CloseTableCommand = new RelayCommand(ExecuteCloseTableCommand);
        BackCommand = new RelayCommand(ExecuteBackCommand);
    }

    private void ExecuteBackCommand(object? obj)
    {
        throw new NotImplementedException();
    }

    private void ExecuteCloseTableCommand(object? obj)
    {
        throw new NotImplementedException();
    }
}
