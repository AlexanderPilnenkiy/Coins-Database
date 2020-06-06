using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Coins_Database.ViewModels
{
    class CoinsListViewModel : Connection
    {
        public List<CoinsList> LoadCoinsList(string Query)
        {
            List<CoinsList> Items = new List<CoinsList>();
            using (var Command = new NpgsqlCommand(Query, Established))
            {
                int LstCount = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Items.Add(new CoinsList()
                    {
                        IDCoin = Convert.ToInt32(Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["id_coin"]),
                        IDEvent = Convert.ToInt32(Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["id_event"]),
                        Type = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["coin_type"].ToString(),
                        Party = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["event_name"].ToString(),
                        Description = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["event_description"].ToString(),
                        Place = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["event_place"].ToString(),
                        Date = Convert.ToDateTime(Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["date"]).ToShortDateString()
                    });
                    i++;
                }
            }
            return Items;
        }
    }
}