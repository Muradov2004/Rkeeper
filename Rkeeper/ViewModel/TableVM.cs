using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using Rkeeper.View.MainWindowComponentsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Rkeeper.Model;

namespace Rkeeper.ViewModel;

class TableVM : BaseVM
{
    private bool IsGridActive = false;

    private ObservableCollection<Food> OrderedFood = new();

    private readonly NavigationStore _navigation;

    public ICommand? TableCommand { get; set; }
    public ICommand? BillCommand { get; set; }
    public ICommand? AddOrderCommand { get; set; }

    public TableVM(NavigationStore navigation)
    {
        _navigation = navigation;
        TableCommand = new RelayCommand(ExecuteTableCommand);
        BillCommand = new RelayCommand(ExecuteBillCommand, CanExecuteBillCommand);
        AddOrderCommand = new RelayCommand(ExecuteAddOrderCommand, CanExecuteAddOrderCommand);
    }

    private bool CanExecuteAddOrderCommand(object? obj) => IsGridActive;


    private void ExecuteAddOrderCommand(object? obj)
    {
        MessageBox.Show("salam");
    }

    private bool CanExecuteBillCommand(object? obj) => true;

    private void ExecuteBillCommand(object? obj)
    {
        MessageBox.Show("sagol");
    }

    private void ExecuteTableCommand(object? obj)
    {
        IsGridActive = true;
        //OrderGrid.
    }
}
