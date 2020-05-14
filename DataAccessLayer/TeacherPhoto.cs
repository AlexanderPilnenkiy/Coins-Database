using System.Windows.Media.Imaging;

namespace Coins_Database.DataAccessLayer
{
    public class TeacherPhoto
    {
        public BitmapImage portrait { get; set; }

        public TeacherPhoto(BitmapImage img)
        {
            portrait = img;
        }
    }
}
