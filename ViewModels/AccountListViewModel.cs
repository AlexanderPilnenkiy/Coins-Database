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
    class AccountListViewModel : Connection
    {
        public List<Account> LoadAccounts(string Query)
        {
            List<Account> Items = new List<Account>();
            using (var Command = new NpgsqlCommand(Query, Established))
            {
                int LstCount = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows.Count;
                int i = 0;
                while (LstCount > i)
                {
                    Items.Add(new Account()
                    {
                        ID = Convert.ToInt32(Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["id_teacher"]),
                        Name = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["teacher_name"].ToString(),
                        Login = Configuration.SDataSet(Command, Established).Tables["LIST"].Rows[i]["Login"].ToString()
                    });
                    i++;
                }
            }
            return Items;
        }
    }
}