using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NUnit.Framework;

namespace Coins_Database.Actions
{
    public class Connection
    {
        public static NpgsqlConnection Established = new NpgsqlConnection();

        public static NpgsqlConnection Connect(string Login, string Password)
        {
            try
            {
                var NewConnection = new NpgsqlConnection(Configuration.LoadSettings(Login, Password));
                NewConnection.Open();
                Established = NewConnection;
                return NewConnection;
            }
            catch
            {
                MessageBox.Show("Неверные параметры подключения");
                return null;
            }
        }

        public static void Disconnect()
        {
            Established.Close();
        }
    }
}
