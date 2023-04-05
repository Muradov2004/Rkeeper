using Rkeeper.View.LoginRegisterView;
using System;
using System.Windows;
using System.Windows.Threading;

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
                    LoginRegisterWindow LoginWindow = new();
                    LoginWindow.Show();

                    Close();
                });
            };

            timer.Start();

        }

	}
}
