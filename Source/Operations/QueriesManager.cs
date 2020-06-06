using Coins_Database.Actions;
using Npgsql;

namespace Coins_Database.Operations
{
    class QueriesManager : Connection
    {
        public static void Execute(string Query)
        {
            using (var command =
                new NpgsqlCommand(Query, Established))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
