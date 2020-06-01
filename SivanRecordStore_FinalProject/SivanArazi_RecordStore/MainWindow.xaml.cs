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

        private void GoToCustomersWin(object sender, RoutedEventArgs e)
        {
            CustomersWin w = new CustomersWin(this);
            w.Show();
            this.Hide();
        }

        private void GoToStockWin(object sender, RoutedEventArgs e)
        {
            StockWin w = new StockWin(this);
            w.Show();
            this.Hide();
        }

        private void GoToSuppliersWin(object sender, RoutedEventArgs e)
        {
            SuppliersWin w = new SuppliersWin(this);
            w.Show();
            this.Hide();
        }

        private void GoToAlbusWin(object sender, RoutedEventArgs e)
        {
            AlbumsWin w = new AlbumsWin(this);
            w.Show();
            this.Hide();
        }

        private void GoToExpensesWin(object sender, RoutedEventArgs e)
        {
            ExpensesWin w = new ExpensesWin(this);
            w.Show();
            this.Hide();
        }

        private void GoToOrdersFromSuppliersWin(object sender, RoutedEventArgs e)
        {
            OrdersFromSuppliersWin w = new OrdersFromSuppliersWin(this);
            w.Show();
            this.Hide();
        }

        private void GoToDealsWin(object sender, RoutedEventArgs e)
        {
            DealsWin w = new DealsWin(this);
            w.Show();
            this.Hide();
        }
    }
}
