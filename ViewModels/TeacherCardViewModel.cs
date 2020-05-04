using Coins_Database.DataAccessLayer;
using GalaSoft.MvvmLight;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Coins_Database.ViewModels
{
    public class TeacherCardViewModel : ViewModelBase
    {
        public ObservableCollection<TeacherCard> LoadTeacherCard(string login, string password, string query)
        {
            using (var connection =
                new NpgsqlConnection($"Server = 127.0.0.1; User Id = {login}; Database = postgres; " +
                $"Port = 5432; Password = {password}"))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows) return null;
                        ObservableCollection<TeacherCard> collection =
                            new ObservableCollection<TeacherCard>();
                        while (reader.Read())
                        {
                            string name = reader.GetString(0).TrimEnd();
                            string spec = reader.GetString(1).TrimEnd();
                            string about = reader.GetString(2).TrimEnd();
                            collection.Add(new TeacherCard(name, spec, about));
                        }
                        return collection;
                    }
                }
            }
        }
    }
}
