using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.Operations
{
    class Operations
    {
        public static void Execute(string login, string password, string query)
        {
            using (var connection =
                new NpgsqlConnection($"Server = 127.0.0.1; User Id = {login}; Database = postgres; " +
                $"Port = 5432; Password = {password}"))
            {
                connection.Open();
                using (var command =
                    new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
