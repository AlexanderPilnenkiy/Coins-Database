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
        public List<Rating> LoadRating(string Login, string Password, string Query)
        {
            List<Rating> Items = new List<Rating>();
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
                        Items.Add(new Rating()
                        {
                            ID = Convert.ToInt32(Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["id_teacher"]),
                            FIO = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                            Coin = Convert.ToInt32(Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["count"])
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
