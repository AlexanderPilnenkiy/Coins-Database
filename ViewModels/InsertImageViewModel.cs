using Coins_Database.Actions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.ViewModels
{
    class InsertImageViewModel
    {
        public void InsertImage(string login, string password, string filepath)
        {
            var connection =
               new NpgsqlConnection(Configuration.LoadSettings(login, password));
            FileStream pgFileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            BinaryReader pgReader = new BinaryReader(new BufferedStream(pgFileStream));

            var ImgByteA = pgReader.ReadBytes(Convert.ToInt32(pgFileStream.Length));

            string sQL = "insert into images (image) VALUES(@Image)";
            using (var command = new NpgsqlCommand(sQL, connection))
            {
                NpgsqlParameter param = command.CreateParameter();
                param.ParameterName = "@Image";
                param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea;
                param.Value = ImgByteA;
                command.Parameters.Add(param);
                connection.Close();
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
