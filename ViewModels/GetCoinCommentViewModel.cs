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
        public string GetCoinComment(string Login, string Password, int IDCoin)
        {
            string Items = "";
            using (var Connection =
                new NpgsqlConnection(Configuration.LoadSettings(Login, Password)))
            {
                Connection.Open();
                using (var Command = new NpgsqlCommand(Queries.GetCoinComment(IDCoin), Connection))
                {
                    int LstCount = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows.Count;
                    int i = 0;
                    Items = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["comment"].ToString();
                }
                Connection.Close();
            }
            return Items;
        }
    }
}
