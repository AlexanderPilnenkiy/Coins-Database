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
    class AdminMessageListViewModel : ViewModelBase
    {
        public List<AdminMessageList> LoadMAList(string Login, string Password, string Query)
        {
            List<AdminMessageList> Items = new List<AdminMessageList>();
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
                        Items.Add(new AdminMessageList()
                        {
                            ID = Convert.ToInt32(Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["id_message"]),
                            Teacher = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                            Event = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["event_name"].ToString(),
                            Date = Convert.ToDateTime(Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["date"]).ToShortDateString(),
                            Status = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["status"].ToString()
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
