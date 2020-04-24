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

namespace SivanArazi_RecordStore
{
    /// <summary>
    /// Interaction logic for StockWin.xaml
    /// </summary>
    public partial class StockWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        public StockWin()
        {
            InitializeComponent();
            ShowStockInfo();
        }

        private void ShowStockInfo()
        {
            
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
