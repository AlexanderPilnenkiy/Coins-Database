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
                        items.Add(new TeachersList()
                        {
                            id_teacher = Convert.ToInt32(iDataSet.Tables["LIST"].Rows[i]["id_teacher"]),
                            FIO = iDataSet.Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                            speciality = iDataSet.Tables["LIST"].Rows[i]["speciality"].ToString()
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
