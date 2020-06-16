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
            //check valid text
            try
            {
                //create new Customer object
                CustomersTBL a = new CustomersTBL
                {
                    Name = tb_A_Name.Text,
                    Phone = tb_A_Phone.Text,
                    Email = tb_A_Email.Text,
                    Address = tb_A_Address.Text,
                };

                db.CustomersTBL.Add(a);

                UpdateProgram();

                //graphic set
                tb_A_Name.Text = "";
                tb_A_Phone.Text = "";
                tb_A_Email.Text = "";
                tb_A_Address.Text = "";
            }
            catch
            {
                ErrorWin w = new ErrorWin("adding customer fail. check text");
                w.Show();
            }
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            //locate the Customer object using id
            int id = int.Parse((cb_D_Customers.Text).Remove((cb_D_Customers.Text).IndexOf(".")));
            db.CustomersTBL.Remove(db.CustomersTBL.Where(i => i.Id == id).First());
            
            UpdateProgram();
        }

        private void UpdateChooseCustomer(object sender, RoutedEventArgs e)
        {
            //graphic set
            tb_U_Name.Visibility = Visibility.Visible;
            tb_U_Phone.Visibility = Visibility.Visible;
            tb_U_Email.Visibility = Visibility.Visible;
            tb_U_Address.Visibility = Visibility.Visible;
            tbl_U_Name.Visibility = Visibility.Visible;
            tbl_U_Phone.Visibility = Visibility.Visible;
            tbl_U_Email.Visibility = Visibility.Visible;
            tbl_U_Address.Visibility = Visibility.Visible;
            b_U.Visibility = Visibility.Visible;

            //locate the Customer object using id
            int id = int.Parse((cb_U_Customers.Text).Remove((cb_U_Customers.Text).IndexOf(".")));
            CustomersTBL cs = db.CustomersTBL.Where(i => i.Id == id).First();

            //write the current object variables
            tb_U_Name.Text = cs.Name;
            tb_U_Phone.Text = cs.Phone;
            tb_U_Email.Text = cs.Email;
            tb_U_Address.Text = cs.Address;
        }

        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            //locate the Customer object using id
            int id = int.Parse((cb_U_Customers.Text).Remove((cb_U_Customers.Text).IndexOf(".")));
            CustomersTBL cs = db.CustomersTBL.Where(i => i.Id == id).First();

            //check valid text
            try
            {
                //update
                cs.Name = tb_U_Name.Text;
                cs.Phone = tb_U_Phone.Text;
                cs.Email = tb_U_Email.Text;
                cs.Address = tb_U_Address.Text;

                //graphic set
                tb_U_Name.Visibility = Visibility.Hidden;
                tb_U_Phone.Visibility = Visibility.Hidden;
                tb_U_Email.Visibility = Visibility.Hidden;
                tb_U_Address.Visibility = Visibility.Hidden;
                tbl_U_Name.Visibility = Visibility.Hidden;
                tbl_U_Phone.Visibility = Visibility.Hidden;
                tbl_U_Email.Visibility = Visibility.Hidden;
                tbl_U_Address.Visibility = Visibility.Hidden;
                b_U.Visibility = Visibility.Hidden;

                tb_U_Name.Text = "";
                tb_U_Phone.Text = "";
                tb_U_Email.Text = "";
                tb_U_Address.Text = "";

                UpdateProgram();
            }
            catch
            {
                ErrorWin w = new ErrorWin("update customer fail. check text");
                w.Show();
            }
        }

        public List<CustomersTBL> getCustomers()
        {
            return db.CustomersTBL.ToList<CustomersTBL>();
        }

        public List<string> getCustomersIdsAndNames()
        {
            return db.CustomersTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        //updating db and evry wpf object that use db info
        public void UpdateProgram()
        {
            db.SaveChanges();
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
