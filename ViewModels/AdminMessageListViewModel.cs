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
        public List<AdminMessageList> LoadMAList(string login, string password, string query)
        {
            List<AdminMessageList> items = new List<AdminMessageList>();
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
                        items.Add(new AdminMessageList()
                        {
                            id_message = Convert.ToInt32(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["id_message"]),
                            teacher = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                            _event = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["event_name"].ToString(),
                            date = Convert.ToDateTime(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["date"]).ToShortDateString(),
                            status = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["status"].ToString()
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
