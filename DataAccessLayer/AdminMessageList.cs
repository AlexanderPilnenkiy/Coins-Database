using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.DataAccessLayer
{
    class AdminMessageList
    {
        public int id_message { get; set; }
        public string teacher { get; set; }
        public string _event { get; set; }
        public string date { get; set; }
        public string status { get; set; }
    }
}
