using Rkeeper.Stores;
using Rkeeper.View.LoginRegisterView;
using Rkeeper.ViewModel;
using System.Windows;

namespace Rkeeper.View.OpeningLoadingView
{
    /// <summary>
    /// Interaction logic for OpeningWindow.xaml
    /// </summary>
    public partial class OpeningWindow : Window
	{

		public OpeningWindow()
		{

			InitializeComponent();

			System.Timers.Timer timer = new()
			{
				Interval = 3000
			};

			timer.Elapsed += (sender, args) =>
			{

				timer.Stop();
				
				Application.Current.Dispatcher.Invoke(() =>
				{
					NavigationStore _navigationStore = new();
					_navigationStore.CurrentVM = new LoginVM(_navigationStore);
					LoginRegisterWindow LoginWindow = new LoginRegisterWindow()
					{
						DataContext = new LoginRegisterVM(_navigationStore)
					};
					LoginWindow.Show();

					Close();
				});
			};

			timer.Start();

		}

	}
}
