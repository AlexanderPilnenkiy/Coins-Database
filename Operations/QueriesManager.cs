using Coins_Database.Actions;
using Npgsql;

namespace Coins_Database.Operations
{
    class QueriesManager
    {
        public static void Execute(NpgsqlConnection Connection, string Query)
        {
            using (var command =
                new NpgsqlCommand(Query, Connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
