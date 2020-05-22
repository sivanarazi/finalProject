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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SivanArazi_RecordStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToEmployeesWin(object sender, RoutedEventArgs e)
        {
            EmployeesWin w = new EmployeesWin(this);
            w.Show();
            this.Hide();
        }

        private void GoToCoustomersWin(object sender, RoutedEventArgs e)
        {
            CoustomersWin w = new CoustomersWin(this);
            w.Show();
            this.Hide();
        }

        private void GoToStockWin(object sender, RoutedEventArgs e)
        {
            StockWin w = new StockWin(this);
            w.Show();
            this.Hide();
        }
        
        private void GoToDealsWin(object sender, RoutedEventArgs e)
        {

        }

        private void GoToSuppliersWin(object sender, RoutedEventArgs e)
        {

        }

        private void GoToAlbusWin(object sender, RoutedEventArgs e)
        {

        }

        private void GoToExpensesWin(object sender, RoutedEventArgs e)
        {

        }
    }
}
