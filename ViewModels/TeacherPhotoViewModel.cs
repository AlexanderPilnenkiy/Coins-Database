using Coins_Database.DataAccessLayer;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Coins_Database.ViewModels
{
    class TeacherPhotoViewModel
    {
        public BitmapFrame LoadTeacherPhoto(string login, string password, string query)
        {
            using (var connection =
                new NpgsqlConnection($"Server = 127.0.0.1; User Id = {login}; Database = postgres; " +
                $"Port = 5432; Password = {password}"))
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
