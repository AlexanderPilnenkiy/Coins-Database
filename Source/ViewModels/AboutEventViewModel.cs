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
    class AboutEventViewModell : Connection
    {
        public List<AboutEvent> LoadEvents(string Query)
        {
            List<AboutEvent> Items = new List<AboutEvent>();
            using (var Command = new NpgsqlCommand(Query, Established))
            {
                int LstCount = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Items.Add(new AboutEvent()
                    {
                        Description = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["about_event"].ToString()
                    });
                    i++;
                }
            }
            return Items;
        }
    }
}
