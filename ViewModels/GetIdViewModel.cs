using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.ViewModels
{
    class GetIdViewModel
    {
        public int LoadID(NpgsqlConnection Connection, string Query, string Col)
        {
            int Items = new int();
            using (var Command = new NpgsqlCommand(Query, Connection))
            {
                int LstCount = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Items = Convert.ToInt32(Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i][Col]);
                    i++;
                }
            }
            return Items;
        }
    }
}