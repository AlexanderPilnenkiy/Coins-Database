using Coins_Database.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Coins_Database.Operations
{
    class DetermineCoinType
    {
        public int CoinType(string cbText)
        {
            if (cbText == "Арткоин")
            {
                return 0;
            }
            else
            {
                if (cbText == "Талант")
                {
                    return 1;
                }
                else
                {
                    if (cbText == "Соц. активность")
                    {
                        return 2;
                    }
                    else
                    {
                        if (cbText == "Интеллект")
                        {
                            return 3;
                        }
                    }
                }
                return -1;
            }
        }
    }
}
