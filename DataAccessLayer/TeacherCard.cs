namespace Coins_Database.DataAccessLayer
{
    public class TeacherCard
    {
        public string FIO { get; set; }
        public string Speciality { get; set; }
        public string AboutTeacher { get; set; }

        public TeacherCard(string Name, string Spec, string About)
        {
            FIO = Name;
            Speciality = Spec;
            AboutTeacher = About;
        }
    }
}
