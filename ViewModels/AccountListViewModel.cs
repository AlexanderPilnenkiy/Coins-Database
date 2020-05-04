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
                new NpgsqlConnection($"Server = 127.0.0.1; User Id = {login}; Database = postgres; " +
                $"Port = 5432; Password = {password}"))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Connection = connection;
                    NpgsqlDataAdapter iAdapter = new NpgsqlDataAdapter(command);
                    DataSet iDataSet = new DataSet();
                    iAdapter.Fill(iDataSet, "LIST");
                    int lstCount = iDataSet.Tables["LIST"].Rows.Count;
                    int i = 0;
                    while (lstCount > i)
                    {
                        items.Add(new Account()
                        {
                            ac_id = Convert.ToInt32(iDataSet.Tables["LIST"].Rows[i]["id_teacher"]),
                            ac_name = iDataSet.Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                            ac_login = iDataSet.Tables["LIST"].Rows[i]["login"].ToString()
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
