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
    class AdminMessageListViewModel : Connection
    {
        public List<AdminMessageList> LoadMAList(string Query)
        {
            List<AdminMessageList> Items = new List<AdminMessageList>();
            using (var Command = new NpgsqlCommand(Query, Established))
            {
                int LstCount = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Items.Add(new AdminMessageList()
                    {
                        ID = Convert.ToInt32(Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["id_message"]),
                        Teacher = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                        Event = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["event_name"].ToString(),
                        Date = Convert.ToDateTime(Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["date"]).ToShortDateString(),
                        Status = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["status"].ToString()
                    });
                    i++;
                }
            }
            return Items;
        }
    }
}
