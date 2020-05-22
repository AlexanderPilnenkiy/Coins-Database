using Coins_Database.Actions;
using Npgsql;
using System.IO;
using System.Windows.Media.Imaging;

namespace Coins_Database.ViewModels
{
    class TeacherPhotoViewModel
    {
        public BitmapFrame LoadTeacherPhoto(NpgsqlConnection Connection, string Query)
        {
            using (var Command = new NpgsqlCommand(Query, Connection))
            {
                byte[] ProductImageByte = null;
                var Rdr = Command.ExecuteReader();
                if (Rdr.Read())
                {
                    ProductImageByte = (byte[])Rdr[0];
                }
                Rdr.Close();
                if (ProductImageByte != null)
                {
                    using (MemoryStream productImageStream = new MemoryStream(ProductImageByte))
                    {
                        var Bitmap = BitmapFrame.Create(productImageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                        return Bitmap;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
