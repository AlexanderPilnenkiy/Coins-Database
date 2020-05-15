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
        public List<Events> LoadEvents(string Login, string Password, string Query)
        {
            List<Events> Items = new List<Events>();
            using (var Connection =
                new NpgsqlConnection(Configuration.LoadSettings(Login, Password)))
            {
                Connection.Open();
                using (var Command = new NpgsqlCommand(Query, Connection))
                {
                    int LstCount = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows.Count;
                    int i = 0;
                    while (LstCount > i)
                    {
                        Items.Add(new Events()
                        {
                            Caption = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["event_name"].ToString(),
                            Type = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["event_type"].ToString(),
                            Place = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["event_place"].ToString(),
                            Date = Convert.ToDateTime(Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["date"]).ToShortDateString()
                        });
                        i++;
                    }
                }
                Connection.Close();
            }
            return Items;
        }
    }
}