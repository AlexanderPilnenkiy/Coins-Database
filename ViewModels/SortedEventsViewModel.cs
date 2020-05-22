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
    class SortedEventsViewModel : Connection
    {
        public List<Events> LoadSortedEvents(string Query)
        {
            List<Events> Items = new List<Events>();
            using (var Command = new NpgsqlCommand(Query, Established))
            {
                int LstCount = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Items.Add(new Events()
                    {
                        Caption = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["event_name"].ToString(),
                        Type = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["event_type"].ToString(),
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
