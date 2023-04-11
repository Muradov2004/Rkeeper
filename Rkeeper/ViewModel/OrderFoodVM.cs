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

class OrderFoodVM : BaseVM
{
    public ObservableCollection<Food> OrderedFood { get; set; } = new();
    public string? TableName { get; set; }
    public List<Food> MenuFood { get; set; }

    private readonly NavigationStore _navigation;

    public ICommand? DoneCommand { get; set; }
    public ICommand? AddListCommand { get; set; }
    public ICommand? RemoveCommand { get; set; }

    public OrderFoodVM(NavigationStore navigation)
    {
        _navigation = navigation;
        MenuFood = new FoodMenu().MenuFoods.ToList();
        DoneCommand = new RelayCommand(ExecuteDoneCommand);
        AddListCommand = new RelayCommand(ExecuteAddListCommand);
        RemoveCommand = new RelayCommand(ExecuteRemoveCommand);
    }

    private void ExecuteRemoveCommand(object? obj)
    {

        Food? foodToRemove = OrderedFood.FirstOrDefault(f => f.Name == obj?.ToString());
        OrderedFood.Remove(foodToRemove);

    }

    private void ExecuteAddListCommand(object? obj)
    {
        foreach (var food in MenuFood)
        {
            if (food.Name == obj?.ToString())
            {
                OrderedFood.Add(food);
                break;
            }
        }
    }

    private void ExecuteDoneCommand(object? obj)
    {
        CombineOrder();
        AddNewOrderToJson();
        _navigation.CurrentVM = new TableVM(_navigation);
    }

    private string GetImageSource(string foodName)
    {
        string imageSource = "";
        foreach (var item in OrderedFood)
        {
            if (item.Name == foodName)
            {
                imageSource = item.ImageSource[41..];
                break;
            }
        }
        return imageSource;

    }

    private void CombineOrder()
    {
        var groupedFoods = OrderedFood
                            .GroupBy(f => f.Name)
                            .Select(g => new Food(g.Key, g.Average(f => f.Price), GetImageSource(g.Key), g.Sum(f => f.Count)))
                            .ToList();

        OrderedFood.Clear();
        foreach (var food in groupedFoods)
            OrderedFood.Add(food);

    }

    private void AddNewOrderToJson()
    {
        Dictionary<string, ObservableCollection<Food>>? TableOrderedFood = new();
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\TableOrderFood.json";
        string TableOrderedFoodJson = File.ReadAllText(path);
        TableOrderedFood = JsonConvert.DeserializeObject<Dictionary<string, ObservableCollection<Food>>>(TableOrderedFoodJson);
        foreach (var key in TableOrderedFood.Keys)
        {
            if (key == TableName)
            {
                TableOrderedFood[key].Clear();
                foreach (var item in OrderedFood)
                    TableOrderedFood[key].Add(item);
            }
        }
        string json = JsonConvert.SerializeObject(TableOrderedFood, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}
