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
                new NpgsqlConnection($"Server = 127.0.0.1; User Id = {login}; Database = postgres; " +
                $"Port = 5432; Password = {password}"))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(Queries.GetLastImage, connection))
                {
                    command.Connection = connection;
                    NpgsqlDataAdapter iAdapter = new NpgsqlDataAdapter(command);
                    DataSet iDataSet = new DataSet();
                    iAdapter.Fill(iDataSet, "LIST");
                    int lstCount = iDataSet.Tables["LIST"].Rows.Count;
                    int i = 0;
                    while (lstCount > i)
                    {
                        items = Convert.ToInt32(iDataSet.Tables["LIST"].Rows[i]["max"]);
                        i++;
                    }
                }
                connection.Close();
            }
            return items;
        }
    }
}
