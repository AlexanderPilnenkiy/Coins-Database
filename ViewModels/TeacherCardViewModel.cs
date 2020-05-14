using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using GalaSoft.MvvmLight;
using Npgsql;
using System.Collections.ObjectModel;

namespace Coins_Database.ViewModels
{
    public class TeacherCardViewModel : ViewModelBase
    {
        public ObservableCollection<TeacherCard> LoadTeacherCard(string login, string password, string query)
        {
            using (var connection =
                new NpgsqlConnection(Configuration.LoadSettings(login, password)))
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
