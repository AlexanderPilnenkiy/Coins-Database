using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using GalaSoft.MvvmLight;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.ViewModels
{
    class EventsViewModel : ViewModelBase
    {
        public List<Events> LoadEvents(string login, string password, string query)
        {
            List<Events> items = new List<Events>();
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
                        items.Add(new Events()
                        {
                            caption = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["event_name"].ToString(),
                            type = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["event_type"].ToString(),
                            place = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["event_place"].ToString(),
                            date = Convert.ToDateTime(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["date"]).ToShortDateString()
                        }) ;
                        i++;
                    }
                }
                connection.Close();
            }
            return items;
        }
    }
}
