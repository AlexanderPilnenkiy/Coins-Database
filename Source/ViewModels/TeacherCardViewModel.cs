using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using GalaSoft.MvvmLight;
using Npgsql;
using System.Collections.ObjectModel;

namespace Coins_Database.ViewModels
{
    class TeacherCardViewModel : Connection
    {
        public ObservableCollection<TeacherCard> LoadTeacherCard(string Query)
        {
                using (var Command = new NpgsqlCommand(Query, Established))
                {
                using (var Reader = Command.ExecuteReader())
                {
                    if (!Reader.HasRows) return null;
                    ObservableCollection<TeacherCard> Collection =
                        new ObservableCollection<TeacherCard>();
                    while (Reader.Read())
                    {
                        Collection.Add(new TeacherCard(Reader.GetString(0).TrimEnd(),
                            Reader.GetString(1).TrimEnd(),
                            Reader.GetString(2).TrimEnd()));
                    }
                    return Collection;
                }
            }
        }
    }
}
