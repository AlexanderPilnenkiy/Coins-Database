using Coins_Database.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Coins_Database.Views
{
    public partial class ConnectionParametersWindow : Window
    {
        public ConnectionParametersWindow()
        {
            InitializeComponent();
            textBoxIp.Text = XML.ReadXML(XML.CheckOrCreateXML())[0];
            textBoxPort.Text = XML.ReadXML(XML.CheckOrCreateXML())[1];
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Если вы что-то сломали и не знаете, как вернуть, то стандартные настройки:\nПорт - 5432\nIP - 127.0.0.1");
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            XML.RewriteXML(textBoxPort.Text, textBoxIp.Text);
            Close();
        }
    }
}
