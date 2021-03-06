﻿using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using GalaSoft.MvvmLight;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Coins_Database.ViewModels
{
    class TeachersListViewModel : Connection
    {
        public List<TeachersList> LoadTeachersList(string Query)
        {
            List<TeachersList> Items = new List<TeachersList>();
            using (var Command = new NpgsqlCommand(Query, Established))
            {
                int LstCount = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Items.Add(new TeachersList()
                    {
                        ID = Convert.ToInt32(Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["id_teacher"]),
                        FIO = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                        Speciality = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["speciality"].ToString()
                    });
                    i++;
                }
            }
            return Items;
        }
    }
}
