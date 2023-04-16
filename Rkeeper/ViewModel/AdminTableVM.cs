using Rkeeper.Model;
using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System.Windows.Controls;
using System;
using System.Windows.Input;
using Org.BouncyCastle.Utilities.Collections;
using System.Collections.Generic;

namespace Rkeeper.ViewModel;

internal class AdminTableVM : BaseVM
{

    public List<TableShape> tableShapes = new()
    {
        TableShape.Circle,
        TableShape.Square,
        TableShape.Rectangle
    };

    private string? _tableName;

    public string? TableName
    {
        get => _tableName;
        set
        {
            if (_tableName != value)
            {
                _tableName = value;
                NotifyPropertyChanged(nameof(TableName));
            }
        }
    }

    private string? _numberOfChairs;

    public string? NumberOfChairs
    {
        get => _numberOfChairs;
        set
        {
            if (_numberOfChairs != value)
            {
                _numberOfChairs = value;
                NotifyPropertyChanged(nameof(NumberOfChairs));
            }
        }
    }

    private TableShape _selectedShape;

    public TableShape SelectedShape
    {
        get => _selectedShape;
        set
        {
            if (_selectedShape != value)
            {
                _selectedShape = value;
                NotifyPropertyChanged(nameof(SelectedShape));
            }
        }
    }

    public TableCollection TableCollection { get; set; } = new();

    private readonly NavigationStore _navigation;

    public ICommand? AddCommand { get; set; }
    public ICommand? BackCommand { get; set; }
    public AdminTableVM(NavigationStore navigation)
    {
        _navigation = navigation;

        AddCommand = new RelayCommand(ExecuteAddCommand, CanExecuteAddCommand);
        BackCommand = new RelayCommand(ExecuteBackCommand);
    }

    private void ExecuteBackCommand(object? obj)
    {
        _navigation.CurrentVM = new AdminVM(_navigation);
    }

    private bool CanExecuteAddCommand(object? obj)
    {
        return !(string.IsNullOrEmpty(_tableName) || !int.TryParse(_numberOfChairs, out _) || (_selectedShape != default));
    }

    private void ExecuteAddCommand(object? obj)
    {

        Table table = new(TableName, Convert.ToInt32(NumberOfChairs), SelectedShape);
        TableName = "";
        SelectedShape = default;
        NumberOfChairs = "";

    }

}
