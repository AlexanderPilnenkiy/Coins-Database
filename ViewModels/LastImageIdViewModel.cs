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
    class LastImageIdViewModel
    {
        public int LastImageID(string Login, string Password)
        {
            int Items = new int();
            using (var Connection =
                new NpgsqlConnection(Configuration.LoadSettings(Login, Password)))
            {
                Connection.Open();
                using (var Command = new NpgsqlCommand(Queries.GetLastImage, Connection))
                {
                    int LstCount = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows.Count;
                    int i = 0;
                    while (LstCount > i)
                    {
                        Items = Convert.ToInt32(Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i]["max"]);
                        i++;
                    }
                }
                Connection.Close();
            }
            return Items;
        }
    }
}
