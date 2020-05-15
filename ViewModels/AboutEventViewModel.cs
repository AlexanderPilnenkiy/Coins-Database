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
        public List<AboutEvent> LoadEvents(string Login, string Password, string Query)
        {
            List<AboutEvent> Items = new List<AboutEvent>();
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
                        Items.Add(new AboutEvent()
                        {
                            Description = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["about_event"].ToString()
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
