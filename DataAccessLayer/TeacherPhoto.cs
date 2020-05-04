using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
