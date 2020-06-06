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
    class CoinsCountViewModel : Connection
    {
        public List<CoinsCount> LoadCoinsCount(string Query)
        {
            List<CoinsCount> Items = new List<CoinsCount>();
            using (var Command = new NpgsqlCommand(Query, Established))
            {
                int LstCount = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Items.Add(new CoinsCount()
                    {
                        Artcoins = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["artcoin"].ToString(),
                        SocActive = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["soc_activity"].ToString(),
                        Talents = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["talent"].ToString(),
                        Intellect = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["intellect"].ToString()
                    });
                    i++;
                }
            }
            return Items;
        }
    }
}
