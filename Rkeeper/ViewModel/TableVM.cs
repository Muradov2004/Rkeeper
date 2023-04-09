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
using System.Windows.Controls;

namespace Rkeeper.ViewModel;

class TableVM : BaseVM
{
    private bool IsListActive = false;

    public Dictionary<string, ObservableCollection<Food>> TableOrderedFood { get; set; }

    public ObservableCollection<Food> OrderedFood { get; set; }

    private readonly NavigationStore _navigation;

    public ICommand? TableCommand { get; set; }
    public ICommand? BillCommand { get; set; }
    public ICommand? AddOrderCommand { get; set; }

    public TableVM(NavigationStore navigation)
    {
        _navigation = navigation;
        TableOrderedFood = new()
        {
            { "Table1" , new() { new("Pizza", 10) { Count = 1 } } },
            { "Table2" , new() { new("Pasta", 12) { Count = 1 } } },
            { "Table3" , new() { new("Steak", 20) { Count = 1 } } },
            { "Table4" , new() { new("Steak", 12) { Count = 1 } } },
            { "Table5" , new() { new("Steak", 11) { Count = 1 } } },
            { "Table6" , new() { new("Steak", 5) { Count = 1 } } },
        };
        OrderedFood = new();
        TableCommand = new RelayCommand(ExecuteTableCommand);
        BillCommand = new RelayCommand(ExecuteBillCommand, CanExecuteBillCommand);
        AddOrderCommand = new RelayCommand(ExecuteAddOrderCommand, CanExecuteAddOrderCommand);
    }

    private bool CanExecuteAddOrderCommand(object? obj) => IsListActive;


    private void ExecuteAddOrderCommand(object? obj)
    {
        MessageBox.Show("salam");
    }

    private bool CanExecuteBillCommand(object? obj) => IsListActive;

    private void ExecuteBillCommand(object? obj)
    {
        MessageBox.Show("sagol");
    }

    private void ExecuteTableCommand(object? obj)
    {
        IsListActive = true;
        Button? button = obj as Button;
        OrderedFood.Clear();
        foreach (var food in TableOrderedFood[$"{button?.Name}"])
            OrderedFood.Add(food);

    }
}
