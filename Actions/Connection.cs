using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coins_Database.Actions
{
    public class Connection
    {
        public static NpgsqlConnection Established = new NpgsqlConnection();

        public static NpgsqlConnection Connect(string Login, string Password)
        {
            var NewConnection = new NpgsqlConnection(Configuration.LoadSettings(Login, Password));
            NewConnection.Open();
            Established = NewConnection;
            return NewConnection;
        }

        public static void Disconnect()
        {
            Established.Close();
        }
    }
}
