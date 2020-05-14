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
    class TeachersListViewModel : ViewModelBase
    {
        public List<TeachersList> LoadTeachersList(string login, string password, string query)
        {
            List<TeachersList> items = new List<TeachersList>();
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
                        items.Add(new TeachersList()
                        {
                            id_teacher = Convert.ToInt32(Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["id_teacher"]),
                            FIO = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                            speciality = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["speciality"].ToString()
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
