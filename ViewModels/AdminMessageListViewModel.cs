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
                        items.Add(new AdminMessageList()
                        {
                            id_message = Convert.ToInt32(iDataSet.Tables["LIST"].Rows[i]["id_message"]),
                            teacher = iDataSet.Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                            _event = iDataSet.Tables["LIST"].Rows[i]["event_name"].ToString(),
                            date = Convert.ToDateTime(iDataSet.Tables["LIST"].Rows[i]["date"]).ToShortDateString(),
                            status = iDataSet.Tables["LIST"].Rows[i]["status"].ToString()
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
