using Coins_Database.Actions;
using Coins_Database.DataAccessLayer;
using GalaSoft.MvvmLight;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.ViewModels
{
    class cbEventsSortViewModel : Connection
    {
        public List<string> LoadTypes(string Query, string Col)
        {
            List<string> Item = new List<string>();
            using (var Command = new NpgsqlCommand(Query, Established))
            {
                int LstCount = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Item.Add(Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i][Col].ToString());
                    i++;
                }
            }
            return Item;
        }
    }
}
