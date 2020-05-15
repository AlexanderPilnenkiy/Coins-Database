using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.ViewModels
{
    class GetIdViewModel
    {
        public int LoadID(string login, string password, string query, string col)
        {
            int items = new int();
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
                        items = Convert.ToInt32(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i][col]);
                        i++;
                    }
                }
                connection.Close();
            }
            return items;
        }
    }
}