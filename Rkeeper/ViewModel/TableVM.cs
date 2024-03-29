﻿using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Rkeeper.Model;
using System.Windows.Threading;
using System.Windows;
using Rkeeper.View.LoginRegisterView;
using System.Linq;

namespace Rkeeper.ViewModel;

class TableVM : BaseVM
{
    private bool IsListActive = false;

    public string Username { get; set; } = "";

    private string? _selectedTable;
    public string? SelectedTable
    {
        get { return _selectedTable; }
        set
        {
            if (_selectedTable != value)
            {
                _selectedTable = value;
                NotifyPropertyChanged(nameof(SelectedTable));
            }
        }
    }

    private string? _time;
    public string? Time
    {
        get { return _time; }
        set
        {
            _time = value;
            NotifyPropertyChanged(nameof(Time));
        }
    }

    public TableCollection TableCollection { get; set; } = new();

    public ObservableCollection<Food> OrderedFood { get; set; } = new();
    public string? SelectedTableName { get; set; }

    private readonly NavigationStore _navigation;

    public ICommand? TableCommand { get; set; }
    public ICommand? BillCommand { get; set; }
    public ICommand? AddOrderCommand { get; set; }
    public ICommand? LogoutCommand { get; set; }

    public TableVM(NavigationStore navigation)
    {

        _navigation = navigation;
        SetClock();
        SelectedTable = "none";
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
        timer.Tick += TimerTick!;
        timer.Start();
    }

    private void TimerTick(object sender, EventArgs e)
    {
        Time = DateTime.Now.ToString("T");
    }

    private bool CanExecuteAddOrderCommand(object? obj) => IsListActive;


    private void ExecuteAddOrderCommand(object? obj)
    {
        ObservableCollection<Food> SeperatedFood = new();
        foreach (Food food in TableCollection.Tables.FirstOrDefault(t => t.Name == SelectedTableName)!.OrderedFood)
        {
            int count = food.Count;
            for (int i = 0; i < count; i++)
            {
                food.Count = 1;
                SeperatedFood.Add(food);
            }
        }
        _navigation.CurrentVM = new OrderFoodVM(_navigation) { OrderedFood = SeperatedFood, TableName = SelectedTableName, username = Username };
    }

    private bool CanExecuteBillCommand(object? obj) => IsListActive && (OrderedFood.Count != 0);

    private void ExecuteBillCommand(object? obj)
    {
        ObservableCollection<Food> TotalOrderedFood = new();
        double totalprice = 0;
        foreach (var item in TableCollection.Tables.FirstOrDefault(t => t.Name == SelectedTableName)!.OrderedFood)
        {
            TotalOrderedFood.Add(item);
            totalprice += item.Price * item.Count;
        }
        TotalOrderedFood.Add(new Food { Name = "Total", Price = Math.Round(totalprice, 2) });
        _navigation.CurrentVM = new BillVM(_navigation) { OrderedFood = TotalOrderedFood, TableName = SelectedTableName, username = Username };
    }

    private void ExecuteTableCommand(object? obj)
    {
        IsListActive = true;
        OrderedFood.Clear();
        SelectedTableName = obj!.ToString();
        SelectedTable = SelectedTableName!;
        foreach (var food in TableCollection.Tables.FirstOrDefault(t => t.Name == SelectedTableName)!.OrderedFood)
            OrderedFood.Add(food);
    }

}
