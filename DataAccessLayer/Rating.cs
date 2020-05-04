using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Coins_Database.DataAccessLayer
{
    public class Rating
    {
        public int id_teacher { get; set; }
        public string FIO { get; set; }
        public int coin { get; set; }
    }
}
