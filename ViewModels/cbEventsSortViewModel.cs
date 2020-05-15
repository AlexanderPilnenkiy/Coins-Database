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
    class cbEventsSortViewModel : ViewModelBase
    {
        public List<string> LoadTypes(string Login, string Password, string Query, string Col)
        {
            List<string> Item = new List<string>();
            using (var Connection =
                new NpgsqlConnection(Configuration.LoadSettings(Login, Password)))
            {
                Connection.Open();
                using (var Command = new NpgsqlCommand(Query, Connection))
                {
                    int LstCount = Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows.Count;
                    int i = 0;
                    while (LstCount > i)
                    {
                        Item.Add(Configuration.SDataSet(Command, Connection).Tables["LIST"].Rows[i][Col].ToString());
                        i++;
                    }
                }
                Connection.Close();
            }
            return Item;
        }
    }
}
