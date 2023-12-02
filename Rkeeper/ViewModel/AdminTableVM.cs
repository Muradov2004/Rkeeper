using MaterialDesignThemes.Wpf;
using Rkeeper.Model;
using Rkeeper.Stores;
using Rkeeper.ViewModel.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Rkeeper.ViewModel;

internal class AdminTableVM : BaseVM
{
    private readonly NavigationStore _navigation;

    public ICommand? OkCommand { get; set; }

    private BaseTheme _selectedTheme;
    public BaseTheme SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            if (_selectedTheme != value)
            {
                _selectedTheme = value;
                NotifyPropertyChanged(nameof(SelectedTheme));
                ApplyTheme();
            }
        }
    }

    public ObservableCollection<BaseTheme> TableThemes { get; }

    public AdminTableVM(NavigationStore navigation)
    {
        _navigation = navigation;

        OkCommand = new RelayCommand(ExecuteOkCommand);

        TableThemes = new ObservableCollection<BaseTheme>
            {
                BaseTheme.Light,
                BaseTheme.Dark
            };
    }

    private void ApplyTheme()
    {
        Application.Current.Resources.MergedDictionaries.Clear();

        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Cyan.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Resources/ApplicationResources.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/UserViewStyles/OrderedFoodListElementStyle.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/UserViewStyles/RemoveBtnStyle.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/UserViewStyles/TableStyle.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/LoginRegisterStyles/TxtBoxStyle.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/UserViewStyles/RemoveBtnStyle.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/LoginRegisterStyles/PswdBoxStyle.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/LoginRegisterStyles/LoginRegisterButtonStyle.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/LoginRegisterStyles/AdditionalBtnStyleLoginRegister.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/UserControlStyle.xaml")
        });
        Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/Styles/WindowStyle.xaml")
        });

        switch (SelectedTheme)
        {
            case BaseTheme.Light:
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml")
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/Styles/LightTheme.xaml")
                });
                break;

            case BaseTheme.Dark:
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml")
                });
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/Styles/DarkTheme.xaml")
                });
                break;


            default:
                break;
        }
    }

    private void ExecuteOkCommand(object? obj)
    {
        _navigation.CurrentVM = new AdminVM(_navigation);
    }

}
