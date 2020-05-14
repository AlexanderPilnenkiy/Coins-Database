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
    class CoinsCountViewModel
    {
        public List<CoinsCount> LoadCoinsCount(string login, string password, string query)
        {
            List<CoinsCount> items = new List<CoinsCount>();
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
                        items.Add(new CoinsCount()
                        {
                            artcoins = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["artcoin"].ToString(),
                            soc_active = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["soc_activity"].ToString(),
                            talents = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["talent"].ToString(),
                            intellect = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["intellect"].ToString()
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
