using Coins_Database.Actions;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Coins_Database.DataAccessLayer;
using GalaSoft.MvvmLight;
using Coins_Database.Views;

namespace Coins_Database.ViewModels
{
    public class RatingViewModel : ViewModelBase
    {
        public List<Rating> LoadRating(string login, string password, string query)
        {
            List<Rating> items = new List<Rating>();
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
                        items.Add(new Rating()
                        {
                            id_teacher = Convert.ToInt32(iDataSet.Tables["LIST"].Rows[i]["id_teacher"]),
                            FIO = iDataSet.Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                            coin = Convert.ToInt32(iDataSet.Tables["LIST"].Rows[i]["count"])
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
