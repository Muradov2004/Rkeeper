using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using Rkeeper.View.MainWindowComponentsView;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Rkeeper.Model;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.IO;

namespace Rkeeper.ViewModel;

class TableVM : BaseVM
{
    private bool IsListActive = false;

    public Dictionary<string, ObservableCollection<Food>> TableOrderedFood { get; set; } = new();

    public ObservableCollection<Food> OrderedFood { get; set; }
    public string? SelectedTableName { get; set; }

    private readonly NavigationStore _navigation;

    public ICommand? TableCommand { get; set; }
    public ICommand? BillCommand { get; set; }
    public ICommand? AddOrderCommand { get; set; }

    public TableVM(NavigationStore navigation)
    {
        _navigation = navigation;
        JsonToTableOrderedFood();
        //TableOrderedFoodToJson();
        OrderedFood = new();
        TableCommand = new RelayCommand(ExecuteTableCommand);
        BillCommand = new RelayCommand(ExecuteBillCommand, CanExecuteBillCommand);
        AddOrderCommand = new RelayCommand(ExecuteAddOrderCommand, CanExecuteAddOrderCommand);
    }

    private bool CanExecuteAddOrderCommand(object? obj) => IsListActive;


    private void ExecuteAddOrderCommand(object? obj)
    {
        _navigation.CurrentVM = new OrderFoodVM(_navigation, OrderedFood, SelectedTableName);
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
        SelectedTableName = button?.Name;
        foreach (var food in TableOrderedFood[$"{button?.Name}"])
            OrderedFood.Add(food);
    }

    private void TableOrderedFoodToJson()
    {
        string json = JsonConvert.SerializeObject(TableOrderedFood, Newtonsoft.Json.Formatting.Indented);

        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\TableOrderFood.json";
        File.WriteAllText(path, json);
    }

    private void JsonToTableOrderedFood()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\TableOrderFood.json";
        string TableOrderedFoodJson = File.ReadAllText(path);
        TableOrderedFood.Clear();
        TableOrderedFood = JsonConvert.DeserializeObject<Dictionary<string, ObservableCollection<Food>>>(TableOrderedFoodJson);
    }
}
