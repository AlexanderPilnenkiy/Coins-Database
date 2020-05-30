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
            Connection.Disconnect();
            Application.Current.Shutdown();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string Login = textBoxLogin.Text;
            string Password = passwordUserPassword.Password;
            Connection.Connect(Login, Password);
            IsEnabled = false;
            LoadingWindow LoadingWindow = new LoadingWindow
            {
                Owner = this,
                Width = Width - 40,
                Height = Height - 40
            };
            LoadingWindow.Show();

            Thread ConnectThread = new Thread(() =>
            {
                if (Configuration.Connect(Login, Password))
                {
                    Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        this.Hide();
                        MainWindow MWindow = new MainWindow(Login, Password);
                        MWindow.Show();
                    }));
                   
                }
            });
            ConnectThread.Start();
            LoadingWindow.Hide();
            IsEnabled = true;
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SignIn.IsEnabled)
                SignIn_Click(SignIn, new RoutedEventArgs());
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            ConnectionParametersWindow connectionParametersWindow = new ConnectionParametersWindow();
            connectionParametersWindow.Show();
        }
    }
}
