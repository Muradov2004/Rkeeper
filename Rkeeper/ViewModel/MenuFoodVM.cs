using Rkeeper.Model;
using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Security.Policy;
using System.Collections.ObjectModel;
using System.Windows;

namespace Rkeeper.ViewModel;

internal class MenuFoodVM : BaseVM
{

    private string? _foodName;

    public string? FoodName
    {
        get => _foodName;
        set
        {
            if (_foodName != value)
            {
                _foodName = value;
                NotifyPropertyChanged(nameof(FoodName));
            }
        }
    }

    private string? _imageLocation;

    public string? ImageLocation
    {
        get => _imageLocation;
        set
        {
            if (_imageLocation != value)
            {
                _imageLocation = value;
                NotifyPropertyChanged(nameof(ImageLocation));
            }
        }
    }

    private string? _price;

    public string? Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                _price = value;
                NotifyPropertyChanged(nameof(Price));
            }
        }
    }

    private string? _count;

    public string? Count
    {
        get => _count;
        set
        {
            if (_count != value)
            {
                _count = value;
                NotifyPropertyChanged(nameof(Count));
            }
        }
    }

    private readonly NavigationStore _navigation;

    public ObservableCollection<Food> MenuFood { get; set; }
    private FoodMenu Menu = new();

    public ICommand? AddCommand { get; set; }
    public ICommand? BackCommand { get; set; }
    public ICommand? RemoveMenuCommand { get; set; }

    public MenuFoodVM(NavigationStore navigation)
    {
        _navigation = navigation;
        MenuFood = Menu.MenuFoods;
        AddCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);
        BackCommand = new RelayCommand(ExecuteBackCommand);
        RemoveMenuCommand = new RelayCommand(ExecuteRemoveMenuCommand);
    }

    private void ExecuteBackCommand(object? obj)
    {
        _navigation.CurrentVM = new AdminVM(_navigation);
    }

    private bool CanExecuteAddCommand(object? obj)
    {
        return !(string.IsNullOrEmpty(_foodName) || string.IsNullOrEmpty(_imageLocation) || string.IsNullOrEmpty(_price) || !IsImageLink(_imageLocation) || string.IsNullOrEmpty(_count) || !double.TryParse(_price, out _) || !int.TryParse(_count, out _));
    }

    private void ExecuteAddCommand(object? obj)
    {

        Food food = new(FoodName, Convert.ToDouble(Price), ImageLocation, Convert.ToInt32(Count));
        Menu.AddFood(food);
        FoodName = "";
        Price = "";
        ImageLocation = "";
        Count = "";

    }

    private void ExecuteRemoveMenuCommand(object? obj)
    {

        Menu.RemoveFood(obj.ToString());

    }

    private bool IsImageLink(string ImageUrl)
    {
        Regex regex = new Regex(@"\b(?:https?://|www\.)\S+\.(?:jpg|jpeg|gif|png)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        return regex.IsMatch(ImageUrl);
    }
}
