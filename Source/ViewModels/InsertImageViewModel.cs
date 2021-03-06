﻿using Coins_Database.Actions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.ViewModels
{
    class InsertImageViewModel : Connection
    {
        public void InsertImage(string Filepath)
        {
            FileStream PGFileStream = new FileStream(Filepath, FileMode.Open, FileAccess.Read);

            BinaryReader PGReader = new BinaryReader(new BufferedStream(PGFileStream));

            var ImgByteA = PGReader.ReadBytes(Convert.ToInt32(PGFileStream.Length));

            string Sql = "insert into images (image) VALUES(@Image)";
            using (var Command = new NpgsqlCommand(Sql, Established))
            {
                NpgsqlParameter Parameter = Command.CreateParameter();
                Parameter.ParameterName = "@Image";
                Parameter.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea;
                Parameter.Value = ImgByteA;
                Command.Parameters.Add(Parameter);
                Established.Close();
                Established.Open();
                Command.ExecuteNonQuery();
            }
        }
    }
}
