using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using Rkeeper.View.MainWindowComponentsView;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Rkeeper.Model;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Threading;
using System.Windows;
using Rkeeper.View.LoginRegisterView;

namespace Rkeeper.ViewModel;

class TableVM : BaseVM
{
    private bool IsListActive = false;

    public string Username { get; set; } = "";
 
    private string _time;
    public string Time
    {
        get { return _time; }
        set
        {
            _time = value;
            NotifyPropertyChanged(nameof(Time));
        }
    }
    public Dictionary<string, ObservableCollection<Food>>? TableOrderedFood { get; set; } = new();

    public ObservableCollection<Food> OrderedFood { get; set; }
    public string? SelectedTableName { get; set; }

    private readonly NavigationStore _navigation;

    public ICommand? TableCommand { get; set; }
    public ICommand? BillCommand { get; set; }
    public ICommand? AddOrderCommand { get; set; }
    public ICommand? LogoutCommand { get; set; }

    public TableVM(NavigationStore navigation)
    {
        _navigation = navigation;
        JsonToTableOrderedFood();
        SetClock();
        OrderedFood = new();
        TableCommand = new RelayCommand(ExecuteTableCommand);
        BillCommand = new RelayCommand(ExecuteBillCommand, CanExecuteBillCommand);
        AddOrderCommand = new RelayCommand(ExecuteAddOrderCommand, CanExecuteAddOrderCommand);
        LogoutCommand = new RelayCommand(ExecuteLogoutCommand);
    }

    private void ExecuteLogoutCommand(object? obj)
    {
        NavigationStore navigationStore = new();

        navigationStore.CurrentVM = new LoginVM(navigationStore);

        LoginRegisterWindow loginRegisterWindow = new LoginRegisterWindow()
        {
            DataContext = new MainVM(navigationStore)
        };
        loginRegisterWindow.Show();

        var window = Window.GetWindow(obj as DependencyObject);

        window?.Close();
    }

    private void SetClock()
    {
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += timer_Tick;
        timer.Start();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        Time = DateTime.Now.ToString("T");
    }

    private bool CanExecuteAddOrderCommand(object? obj) => IsListActive;


    private void ExecuteAddOrderCommand(object? obj)
    {
        ObservableCollection<Food> SeperatedFood = new();
        foreach (Food food in OrderedFood)
        {
            int count = food.Count;
            for (int i = 0; i < count; i++)
            {
                food.Count = 1;
                SeperatedFood.Add(food);
            }
        }
        _navigation.CurrentVM = new OrderFoodVM(_navigation) { OrderedFood = SeperatedFood, TableName = SelectedTableName };
    }

    private bool CanExecuteBillCommand(object? obj) => IsListActive;

    private void ExecuteBillCommand(object? obj)
    {
        ObservableCollection<Food> TotalOrderedFood = new();
        double totalprice = 0;
        foreach (var item in OrderedFood)
        {
            TotalOrderedFood.Add(item);
            totalprice += item.Price * item.Count;
        }
        TotalOrderedFood.Add(new Food { Name = "Total", Price = Math.Round(totalprice, 2) });
        _navigation.CurrentVM = new BillVM(_navigation) { OrderedFood = TotalOrderedFood, TableName = SelectedTableName };
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

    private void JsonToTableOrderedFood()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\TableOrderFood.json";
        string TableOrderedFoodJson = File.ReadAllText(path);
        TableOrderedFood?.Clear();
        TableOrderedFood = JsonConvert.DeserializeObject<Dictionary<string, ObservableCollection<Food>>>(TableOrderedFoodJson);
    }
}
