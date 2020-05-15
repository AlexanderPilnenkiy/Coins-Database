using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Coins_Database.ViewModels
{
    class CoinsListViewModel
    {
        public List<CoinsList> LoadCoinsList(string login, string password, string query)
        {
            List<CoinsList> items = new List<CoinsList>();
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
                        items.Add(new CoinsList()
                        {
                            id_coin = Convert.ToInt32(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["id_coin"]),
                            id_event = Convert.ToInt32(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["id_event"]),
                            type = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["coin_type"].ToString(),
                            party = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["event_name"].ToString(),
                            description = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["event_description"].ToString(),
                            place = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["event_place"].ToString(),
                            date = Convert.ToDateTime(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["date"]).ToShortDateString()
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