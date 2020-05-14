using Coins_Database.Actions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.ViewModels
{
    class GetCoinCommentViewModel
    {
        public string GetCoinComment(string login, string password, int id_coin)
        {
            string items = "";
            using (var connection =
                new NpgsqlConnection(Configuration.LoadSettings(login, password)))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(Queries.GetCoinComment(id_coin), connection))
                {
                    int lstCount = Configuration.SDataSet(command, connection).Tables["LIST"].Rows.Count;
                    int i = 0;
                    items = Configuration.SDataSet(command, connection).Tables["LIST"].Rows[i]["comment"].ToString();
                }
                connection.Close();
            }
            return items;
        }
    }
}
