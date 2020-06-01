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
    /// Interaction logic for CustomersWin.xaml
    /// </summary>
    public partial class CustomersWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        private MainWindow m;
        public CustomersWin(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            UpdateProgram();
        }

        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            CustomersTBL a = new CustomersTBL
            {
                Name = tb_A_Name.Text,
                Phone = tb_A_Phone.Text,
                Email = tb_A_Email.Text,
                Address = tb_A_Address.Text,
            };
            db.CustomersTBL.Add(a);
            db.SaveChanges();
            UpdateProgram();
            tb_A_Name.Text = "";
            tb_A_Phone.Text = "";
            tb_A_Email.Text = "";
            tb_A_Address.Text = "";
            UpdateProgram();
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((cb_D_Customers.Text).Remove((cb_D_Customers.Text).IndexOf(".")));
            db.CustomersTBL.Remove(db.CustomersTBL.Where(i => i.Id == id).First());
            db.SaveChanges();
            UpdateProgram();
        }

        private void UpdateChooseCustomer(object sender, RoutedEventArgs e)
        {
            tb_U_Name.Visibility = Visibility.Visible;
            tb_U_Phone.Visibility = Visibility.Visible;
            tb_U_Email.Visibility = Visibility.Visible;
            tb_U_Address.Visibility = Visibility.Visible;
            tbl_U_Name.Visibility = Visibility.Visible;
            tbl_U_Phone.Visibility = Visibility.Visible;
            tbl_U_Email.Visibility = Visibility.Visible;
            tbl_U_Address.Visibility = Visibility.Visible;

            tb_U_Name.Text = "";
            tb_U_Phone.Text = "";
            tb_U_Email.Text = "";
            tb_U_Address.Text = "";
        }

        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((cb_U_Customers.Text).Remove((cb_U_Customers.Text).IndexOf(".")));
            CustomersTBL cs = db.CustomersTBL.Where(i => i.Id == id).First();
            cs.Name = tb_U_Name.Text;
            cs.Phone = tb_U_Phone.Text;
            cs.Email = tb_U_Email.Text;
            cs.Address = tb_U_Address.Text;

            tb_U_Name.Visibility = Visibility.Hidden;
            tb_U_Phone.Visibility = Visibility.Hidden;
            tb_U_Email.Visibility = Visibility.Hidden;
            tb_U_Address.Visibility = Visibility.Hidden;
            tbl_U_Name.Visibility = Visibility.Hidden;
            tbl_U_Phone.Visibility = Visibility.Hidden;
            tbl_U_Email.Visibility = Visibility.Hidden;
            tbl_U_Address.Visibility = Visibility.Hidden;

            tb_U_Name.Text = "";
            tb_U_Phone.Text = "";
            tb_U_Email.Text = "";
            tb_U_Address.Text = "";

            db.SaveChanges();
            UpdateProgram();
        }

        public List<CustomersTBL> getCustomers()
        {
            return db.CustomersTBL.ToList<CustomersTBL>();
        }

        public List<string> getCustomersIdsAndNames()
        {
            return db.CustomersTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public void UpdateProgram()
        {
            dg_Customers.ItemsSource = getCustomers();
            cb_D_Customers.ItemsSource = getCustomersIdsAndNames();
            cb_U_Customers.ItemsSource = getCustomersIdsAndNames();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }
    }
}
