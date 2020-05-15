using System.Windows.Media.Imaging;

namespace Coins_Database.DataAccessLayer
{
    public class TeacherPhoto
    {
        public BitmapImage Portrait { get; set; }

        public TeacherPhoto(BitmapImage Img)
        {
            Portrait = Img;
        }
    }
}
