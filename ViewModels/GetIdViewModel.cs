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
    class GetIdViewModel : Connection
    {
        public int LoadID(string Query, string Col)
        {
            int Items = new int();
            using (var Command = new NpgsqlCommand(Query, Established))
            {
                int LstCount = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Items = Convert.ToInt32(Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i][Col]);
                    i++;
                }
            }
            return Items;
        }
    }
}