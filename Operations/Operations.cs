using Coins_Database.Actions;
using Npgsql;

namespace Coins_Database.Operations
{
    class Operations
    {
        public static void Execute(string login, string password, string query)
        {
            using (var connection =
                 new NpgsqlConnection(Configuration.LoadSettings(login, password)))
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
