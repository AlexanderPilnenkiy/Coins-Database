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
        public string GetCoinComment(NpgsqlConnection Connection, int IDCoin)
        {
            string Items = "";
            using (var Command = new NpgsqlCommand(Queries.GetCoinComment(IDCoin), Connection))
            {
                int LstCount = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows.Count;
                int i = 0;
                Items = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["comment"].ToString();
            }
            return Items;
        }
    }
}
