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
    class AboutEventViewModell : ViewModelBase
    {
        public List<AboutEvent> LoadEvents(string login, string password, string query)
        {
            List<AboutEvent> items = new List<AboutEvent>();
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
                        items.Add(new AboutEvent()
                        {
                            about_event = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["about_event"].ToString()
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
