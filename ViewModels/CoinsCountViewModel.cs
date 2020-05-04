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
                        items.Add(new CoinsCount()
                        {
                            artcoins = iDataSet.Tables["LIST"].Rows[i]["artcoin"].ToString(),
                            soc_active = iDataSet.Tables["LIST"].Rows[i]["soc_activity"].ToString(),
                            talents = iDataSet.Tables["LIST"].Rows[i]["talent"].ToString(),
                            intellect = iDataSet.Tables["LIST"].Rows[i]["intellect"].ToString()
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
