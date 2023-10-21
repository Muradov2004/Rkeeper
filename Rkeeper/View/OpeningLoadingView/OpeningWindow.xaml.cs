using Rkeeper.Stores;
using Rkeeper.View.LoginRegisterView;
using Rkeeper.ViewModel;
using System.Threading.Tasks;
using System.Windows;

namespace Rkeeper.View.OpeningLoadingView;

/// <summary>
/// Interaction logic for OpeningWindow.xaml
/// </summary>
public partial class OpeningWindow : Window
{

    public OpeningWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            Dispatcher.Invoke(async () =>
            {
                for (int i = 0; i < 100; i++)
                {
                    LoadBar.Value++;
                    await Task.Delay(15);
                }
                NavigationStore _navigationStore = new();

                _navigationStore.CurrentVM = new LoginVM(_navigationStore);

                LoginRegisterWindow LoginWindow = new() { DataContext = new LoginRegisterVM(_navigationStore) };

                LoginWindow.Show();

                Close();

            });

        });

    }
}
