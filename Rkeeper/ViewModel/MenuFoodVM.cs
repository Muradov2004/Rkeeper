using Rkeeper.Model;
using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

internal class MenuFoodVM : BaseVM
{

    private string _foodName;
    private string _imageLocation;
    private string _price;
    private string _count;

    public string FoodName
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

    public string ImageLocation
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

    public string Price
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

    public string Count
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

    public List<Food> MenuFood { get; set; }

    public ICommand? AddCommand { get; set; }
    public ICommand? BackCommand { get; set; }
    public ICommand? RemoveMenuCommand { get; set; }

    public MenuFoodVM(NavigationStore navigation)
    {
        _navigation = navigation;
        MenuFood = new FoodMenu().MenuFoods.ToList();
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
        return !(string.IsNullOrEmpty(_foodName) || string.IsNullOrEmpty(_imageLocation) || string.IsNullOrEmpty(_price) || !IsImage(_imageLocation) || string.IsNullOrEmpty(_count) || !double.TryParse(_price,out _) || !int.TryParse(_count, out _));
    }

    private void ExecuteAddCommand(object? obj)
    {

        string imageName = Path.GetFileName(_imageLocation);
        string destinationPath = AppDomain.CurrentDomain.BaseDirectory[..^25];
        string targetFilePath = Path.Combine(destinationPath, "Assets", "MenuImages", imageName);
        File.Copy(_imageLocation, targetFilePath, true);

    }

    private void ExecuteRemoveMenuCommand(object? obj)
    {
    }

    private bool IsImage(string filename)
    {
        return filename.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
               filename.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
               filename.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
               filename.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase);
    }
}
