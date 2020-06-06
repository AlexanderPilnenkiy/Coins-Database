using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Coins_Database.Controls
{
    class AddCoinTypes
    {
        public void ReloadCB (ComboBox CBox)
        {
            CBox.Items.Clear();
            CBox.Items.Add("Арткоин");
            CBox.Items.Add("Талант");
            CBox.Items.Add("Соц. активность");
            CBox.Items.Add("Интеллект");
        }
    }
}
