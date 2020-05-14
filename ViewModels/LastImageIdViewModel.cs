using Coins_Database.Actions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.ViewModels
{
    class LastImageIdViewModel
    {
        public int LastImageID(string login, string password)
        {
            int items = new int();
            using (var connection =
                new NpgsqlConnection(Configuration.LoadSettings(login, password)))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(Queries.GetLastImage, connection))
                {
                    int lstCount = Configuration.SDataSet(command, connection).Tables["LIST"].Rows.Count;
                    int i = 0;
                    while (lstCount > i)
                    {
                        items = Convert.ToInt32(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["max"]);
                        i++;
                    }
                }
                connection.Close();
            }
            return items;
        }
    }
}
