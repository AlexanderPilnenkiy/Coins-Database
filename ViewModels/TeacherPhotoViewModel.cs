using Coins_Database.Actions;
using Npgsql;
using System.IO;
using System.Windows.Media.Imaging;

namespace Coins_Database.ViewModels
{
    class TeacherPhotoViewModel
    {
        public BitmapFrame LoadTeacherPhoto(string login, string password, string query)
        {
            using (var connection =
                new NpgsqlConnection(Configuration.LoadSettings(login, password)))
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    byte[] productImageByte = null;
                    connection.Open();
                    var rdr = command.ExecuteReader();
                    if (rdr.Read())
                    {
                        productImageByte = (byte[])rdr[0];
                    }
                    rdr.Close();
                    if (productImageByte != null)
                    {
                        using (MemoryStream productImageStream = new MemoryStream(productImageByte))
                        {
                            var bitmap = BitmapFrame.Create(productImageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                            return bitmap;
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
}
