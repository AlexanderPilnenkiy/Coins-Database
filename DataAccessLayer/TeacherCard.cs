using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Coins_Database.DataAccessLayer
{
    public class TeacherCard
    {
        //public Image portrait { get; set; }
        public string fio { get; set; }
        public string speciality { get; set; }
        public string about_teacher { get; set; }

        public TeacherCard(string name, string spec, string about)
        {
            fio = name;
            speciality = spec;
            about_teacher = about;
        }
    }
}
