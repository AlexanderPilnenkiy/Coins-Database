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
    class AccountListViewModel
    {
        public List<Account> LoadAccounts(string login, string password, string query)
        {
            List<Account> items = new List<Account>();
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
                        items.Add(new Account()
                        {
                            ac_id = Convert.ToInt32(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["id_teacher"]),
                            ac_name = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                            ac_login = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["login"].ToString()
                        });
                        i++;
                    }
                }
                connection.Close();
            }
            return items;
        }
    }
}
