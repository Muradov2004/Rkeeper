using System.Windows;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Threading.Tasks;
using System;

namespace Rkeeper.View.OpeningLoadingView;

/// <summary>
/// Interaction logic for Loding.xaml
/// </summary>
public partial class Loding : Window
{

    private List<string>? Folders { get; set; } = new()
    {
        "App.xaml" ,
        "App.xaml.cs" ,
        "AssemblyInfo.cs" ,
        "Rkeeper.csproj" ,
        "Rkeeper.csproj.user" ,
        "Assets/Fonts/password.ttf" ,
        "Assets/Gifs/rkeeper.gif" ,
        "Assets/IconImage/rkeeper.ico" ,
        "Assets/LoginRegisterImages/LoginImage.png" ,
        "Assets/LoginRegisterImages/RegisterImage.png" ,
        "Assets/MenuImages/BuffaloWings.jpg" ,
        "Assets/MenuImages/CaesarSalad.jpg" ,
        "Assets/MenuImages/ChickenBurger.jpg" ,
        "Assets/MenuImages/DefaultMenuFoodImage.jpg" ,
        "Assets/MenuImages/FishChips.jpg" ,
        "Assets/MenuImages/GrilledCheeseSandwich.jpg" ,
        "Assets/MenuImages/MargheritaPizza.jpg" ,
        "Assets/MenuImages/PadThai.jpg" ,
        "Assets/MenuImages/PepperoniPizza.jpg" ,
        "Assets/MenuImages/SpaghettiBolognese.jpg" ,
        "Assets/MenuImages/SushiRoll.jpg" ,
        "JsonFiles/Menu.json" ,
        "JsonFiles/Tables.json" ,
        "JsonFiles/Users.json" ,
        "Model/Food.cs" ,
        "Model/FoodMenu.cs" ,
        "Model/Table.cs" ,
        "Model/User.cs" ,
        "ref/Rkeeper.dll" ,
        "refint/Rkeeper.dll" ,
        "Resources/ApplicationResources.xaml" ,
        "Resources/ApplicationResources.xaml.cs" ,
        "Resources/History.txt" ,
        "Resources/LogFile.txt" ,
        "Stores/NavigationStore.xaml" ,
        "Styles/UserControlStyle.xaml" ,
        "Styles/WindowStyle.xaml" ,
        "Styles/LoginRegisterStyles/AdditionalBtnStyleLoginRegister.xaml" ,
        "Styles/LoginRegisterStyles/LoginRegisterButtonStyle.xaml" ,
        "Styles/LoginRegisterStyles/PswdBoxStyle.xaml" ,
        "Styles/LoginRegisterStyles/TxtBoxStyle.xaml" ,
        "Styles/UserViewStyles/OrderedFoodListElementStyle.xaml" ,
        "Styles/UserViewStyles/RemoveBtnStyle.xaml" ,
        "Styles/UserViewStyles/TableStyle.xaml" ,
        "View/MainView.xaml" ,
        "View/MainView.xaml.cs" ,
        "View/MainView.xaml.cs" ,
        "View/Admin/AdminTableView.xaml" ,
        "View/Admin/AdminTableView.xaml.cs" ,
        "View/Admin/AdminView.xaml" ,
        "View/Admin/AdminView.xaml.cs" ,
        "View/Admin/AdminView.xaml.cs" ,
        "View/Admin/LogfileView.xaml" ,
        "View/Admin/LogfileView.xaml.cs" ,
        "View/Admin/MenuFoodView.xaml" ,
        "View/Admin/MenuFoodView.xaml.cs" ,
        "View/LoginRegisterView/LoginRegisterWindow.xaml" ,
        "View/LoginRegisterView/LoginRegisterWindow.xaml.cs" ,
        "View/LoginRegisterView/LoginView.xaml" ,
        "View/LoginRegisterView/LoginView.xaml.cs" ,
        "View/LoginRegisterView/RegisterView.xaml" ,
        "View/LoginRegisterView/RegisterView.xaml.cs" ,
        "View/MainWindowComponentsView/BillView.xaml" ,
        "View/MainWindowComponentsView/BillView.xaml.cs" ,
        "View/MainWindowComponentsView/OrderFoodView.xaml" ,
        "View/MainWindowComponentsView/OrderFoodView.xaml.cs" ,
        "View/MainWindowComponentsView/TableView.xaml" ,
        "View/MainWindowComponentsView/TableView.xaml.cs" ,
        "View/OpeningLoadingView/Loding.xaml" ,
        "View/OpeningLoadingView/Loding.xaml.cs" ,
        "View/OpeningLoadingView/OpeningWindow.xaml" ,
        "View/OpeningLoadingView/OpeningWindow.xaml.cs" ,
        "ViewModel/AdminTableVM.cs" ,
        "ViewModel/AdminVM.cs" ,
        "ViewModel/BaseVM.cs" ,
        "ViewModel/BillVM.cs" ,
        "ViewModel/LogfileVM.cs" ,
        "ViewModel/LoginRegisterVM.cs" ,
        "ViewModel/LoginVM.cs" ,
        "ViewModel/MainVM.cs" ,
        "ViewModel/MenuFoodVM.cs" ,
        "ViewModel/OrderFoodVM.cs" ,
        "ViewModel/RegisterVM.cs" ,
        "ViewModel/TableVM.cs" ,
        "ViewModel/Command/RelayCommand.cs" ,
    };

    public Loding()
    {
        InitializeComponent();
    }

    public void Load()
    {
        Task.Run(async () =>
        {
            Dispatcher.Invoke(() =>
            {
                LoadingBar.Maximum = Folders!.Count;
            });

            foreach (var item in Folders!)
            {
                Dispatcher.Invoke(() =>
                {
                    folderNameText.Text = item;
                    LoadingBar.Value++;
                });
                await Task.Delay(20);

            }
            Dispatcher.Invoke(() =>
            {
                Close();
            });
        });
    }

    private void Window_Loaded(object sender, RoutedEventArgs e) => Load();
}