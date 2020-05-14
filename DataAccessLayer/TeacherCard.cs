namespace Coins_Database.DataAccessLayer
{
    public class TeacherCard
    {
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
