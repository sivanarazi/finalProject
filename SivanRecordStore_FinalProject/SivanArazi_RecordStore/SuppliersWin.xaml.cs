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
    /// Interaction logic for SuppliersWin.xaml
    /// </summary>
    public partial class SuppliersWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        private MainWindow m;
        public SuppliersWin(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            UpdateProgram();
        }

        private void AddSupplier(object sender, RoutedEventArgs e)
        {
            try
            {
                //create new object
                SuppliersTBL a = new SuppliersTBL
                {
                    Name = tb_A_Name.Text,
                    Phone = tb_A_Phone.Text,
                    Email = tb_A_Email.Text,
                    Address = tb_A_Address.Text,
                };

                db.SuppliersTBL.Add(a);

                //graphic set
                tb_A_Name.Text = "";
                tb_A_Phone.Text = "";
                tb_A_Email.Text = "";
                tb_A_Address.Text = "";

                UpdateProgram();
            }
            catch
            {
                ErrorWin w = new ErrorWin("fail adding supplier.");
            }
        }

        private void DeleteSupplier(object sender, RoutedEventArgs e)
        {
            //locate the object using id + remove it
            int id = int.Parse((cb_D_Suppliers.Text).Remove((cb_D_Suppliers.Text).IndexOf(".")));
            db.SuppliersTBL.Remove(db.SuppliersTBL.Where(i => i.Id == id).First());

            UpdateProgram();
        }

        private void UpdateChooseSupplier(object sender, RoutedEventArgs e)
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

            //locate the object using id
            int id = int.Parse((cb_U_Suppliers.Text).Remove((cb_U_Suppliers.Text).IndexOf(".")));
            SuppliersTBL cs = db.SuppliersTBL.Where(i => i.Id == id).First();

            //write the current object variables
            tb_A_Name.Text = cs.Name;
            tb_U_Phone.Text = cs.Phone;
            tb_U_Email.Text = cs.Email;
            tb_U_Address.Text = cs.Address;
        }

        private void UpdateSupplier(object sender, RoutedEventArgs e)
        {
            try
            {
                //locate the object using id
                int id = int.Parse((cb_U_Suppliers.Text).Remove((cb_U_Suppliers.Text).IndexOf(".")));
                SuppliersTBL cs = db.SuppliersTBL.Where(i => i.Id == id).First();

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

                tb_A_Name.Text = "";
                tb_U_Phone.Text = "";
                tb_U_Email.Text = "";
                tb_U_Address.Text = "";

                UpdateProgram();
            }
            catch
            {
                ErrorWin w = new ErrorWin("fail supplier update.");
            }
        }

        public List<SuppliersTBL> getSuppliers()
        {
            return db.SuppliersTBL.ToList<SuppliersTBL>();
        }

        public List<string> getSuppliersIdsAndNames()
        {
            return db.SuppliersTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        //updating db and evry wpf object that use db info
        public void UpdateProgram()
        {
            db.SaveChanges();
            dg_Suppliers.ItemsSource = getSuppliers();
            cb_D_Suppliers.ItemsSource = getSuppliersIdsAndNames();
            cb_U_Suppliers.ItemsSource = getSuppliersIdsAndNames();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }
    }
}
