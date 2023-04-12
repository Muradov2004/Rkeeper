using Newtonsoft.Json;
using Rkeeper.Model;
using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        Dictionary<string, ObservableCollection<Food>>? TableOrderedFood = new();
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\TableOrderFood.json";
        string TableOrderedFoodJson = File.ReadAllText(path);
        TableOrderedFood = JsonConvert.DeserializeObject<Dictionary<string, ObservableCollection<Food>>>(TableOrderedFoodJson);
        TableOrderedFood?[TableName].Clear();
        string json = JsonConvert.SerializeObject(TableOrderedFood, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(path, json);
        _navigation.CurrentVM = new TableVM(_navigation);
    }
    private void ExecuteBackCommand(object? obj)
    {
        _navigation.CurrentVM = new TableVM(_navigation);
    }
}
