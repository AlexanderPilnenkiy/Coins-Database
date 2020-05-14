using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using GalaSoft.MvvmLight;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.ViewModels
{
    class cbEventsSortViewModel : ViewModelBase
    {
        public List<string> LoadTypes(string login, string password, string query, string col)
        {
            List<string> item = new List<string>();
            using (var connection =
                new NpgsqlConnection(Configuration.LoadSettings(login, password)))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    int lstCount = Configuration.SDataSet(command, connection).Tables["LIST"].Rows.Count;
                    int i = 0;
                    while (lstCount > i)
                    {
                        item.Add(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i][col].ToString());
                        i++;
                    }
                }
                connection.Close();
            }
            return item;
        }
    }
}
