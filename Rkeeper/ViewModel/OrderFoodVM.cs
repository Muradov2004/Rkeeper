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
    public string username;
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
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"Resources\History.txt";
        File.AppendAllText(path, $"{username} removed {foodToRemove.Name} from {TableName} [{DateTime.Now.ToString("G")}]\n");

    }

    private void ExecuteAddListCommand(object? obj)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"Resources\History.txt";
        if (OrderedFood.Count == 0)
            File.AppendAllText(path, $"{username} opened {TableName} [{DateTime.Now.ToString("G")}]\n");

        Food? food = MenuFood.FirstOrDefault(f => f.Name == obj?.ToString());
        OrderedFood.Add(food);
        File.AppendAllText(path, $"{username} added {food.Name} from {TableName} [{DateTime.Now.ToString("G")}]\n");
    }

    private void ExecuteDoneCommand(object? obj)
    {
        CombineOrder();
        AddNewOrderToJson();
        _navigation.CurrentVM = new TableVM(_navigation) { Username = username };
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
        TableCollection TableCollection = new();
        TableCollection.Tables.FirstOrDefault(t => t.Name == TableName).OrderedFood.Clear();
        foreach (var item in OrderedFood)
            TableCollection.Tables.FirstOrDefault(t => t.Name == TableName).OrderedFood.Add(item);
        TableCollection.TablesToJson();
    }
}
