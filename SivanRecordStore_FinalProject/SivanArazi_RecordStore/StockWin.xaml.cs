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
        //the stock shipts when there is a new deal - products deleted,
        //or when there is ane order from supplier - add products.
        //so there is no option for user to edit or update stock.
        //only view

        DataBaseEntities db = new DataBaseEntities();
        private MainWindow m;
        public StockWin(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            UpdateProgram();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }

        public List<StockTBL> getStock()
        {
            return db.StockTBL.ToList<StockTBL>();
        }

        public void UpdateProgram()
        {
            dg_Stock.ItemsSource = getStock();
        }
    }
}
