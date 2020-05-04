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
    class SortedEventsViewModel
    {
        public List<Events> LoadSortedEvents(string login, string password, string query)
        {
            List<Events> items = new List<Events>();
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
                        items.Add(new Events()
                        {
                            caption = iDataSet.Tables["LIST"].Rows[i]["event_name"].ToString(),
                            type = iDataSet.Tables["LIST"].Rows[i]["event_type"].ToString(),
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
