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
using System.Windows.Media;

namespace Rkeeper.ViewModel;

class OrderFoodVM : BaseVM
{
    public ObservableCollection<Food> OrderedFood { get; set; }
    public string? TableName { get; set; }
    public List<Food> MenuFood { get; set; }

    private readonly NavigationStore _navigation;

    public ICommand? DoneCommand { get; set; }
    public ICommand? AddListCommand { get; set; }
    public ICommand? RemoveCommand { get; set; }

    public OrderFoodVM(NavigationStore navigation, ObservableCollection<Food> orderedFood = null, string? tableName = "")
    {
        _navigation = navigation;
        MenuFood = new FoodMenu().MenuFoods.ToList();
        TableName = tableName;
        OrderedFood = orderedFood;
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
                OrderedFood.Add(food);
        }
    }

    private void ExecuteDoneCommand(object? obj)
    {
        _navigation.CurrentVM = new TableVM(_navigation);
    }
}
