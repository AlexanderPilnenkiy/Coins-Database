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
    class CoinsListViewModel
    {
        public List<CoinsList> LoadCoinsList(string login, string password, string query)
        {
            List<CoinsList> items = new List<CoinsList>();
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
                        items.Add(new CoinsList()
                        {
                            id_coin = Convert.ToInt32(iDataSet.Tables["LIST"].Rows[i]["id_coin"]),
                            id_event = Convert.ToInt32(iDataSet.Tables["LIST"].Rows[i]["id_event"]),
                            type = iDataSet.Tables["LIST"].Rows[i]["coin_type"].ToString(),
                            party = iDataSet.Tables["LIST"].Rows[i]["event_name"].ToString(),
                            description = iDataSet.Tables["LIST"].Rows[i]["event_description"].ToString(),
                            place = iDataSet.Tables["LIST"].Rows[i]["event_place"].ToString(),
                            date = Convert.ToDateTime(iDataSet.Tables["LIST"].Rows[i]["date"]).ToShortDateString()
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
