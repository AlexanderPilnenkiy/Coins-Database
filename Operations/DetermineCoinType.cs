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
        public int CoinType(ComboBox cbAddCoinType)
        {
            if (cbAddCoinType.Text == "Арткоин")
            {
                return 0;
            }
            else
            {
                if (cbAddCoinType.Text == "Талант")
                {
                    return 1;
                }
                else
                {
                    if (cbAddCoinType.Text == "Соц. активность")
                    {
                        return 2;
                    }
                    else
                    {
                        if (cbAddCoinType.Text == "Интеллект")
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
