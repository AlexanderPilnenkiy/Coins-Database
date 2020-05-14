using Coins_Database.Actions;
using Coins_Database.Views;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Coins_Database
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;
            IsEnabled = false;
            LoadingWindow loadingWindow = new LoadingWindow
            {
                Owner = this,
                Width = Width - 40,
                Height = Height - 40
            };
            loadingWindow.Show();

            Thread connectThread = new Thread(() =>
            {
                if (Configuration.Connect(login, password))
                {
                    Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        this.Hide();
                        MainWindow mWindow = new MainWindow(login, password);
                        mWindow.Show();
                    }));
                   
                }
                Configuration.Disconnect();
            });
            connectThread.Start();
            loadingWindow.Hide();
            IsEnabled = true;
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SignIn.IsEnabled)
                SignIn_Click(SignIn, new RoutedEventArgs());
        }
    }
}
