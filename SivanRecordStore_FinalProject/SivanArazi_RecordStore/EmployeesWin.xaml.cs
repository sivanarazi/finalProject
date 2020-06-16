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
    /// Interaction logic for EmployeesWin.xaml
    /// </summary>
    public partial class EmployeesWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        private MainWindow m;
        public EmployeesWin(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            UpdateProgram();
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            try {
            //creating new object
            EmployeesTBL a = new EmployeesTBL
            {
                Name = tb_A_Name.Text,
                Phone = tb_A_Phone.Text,
                Email = tb_A_Email.Text,
                Address = tb_A_Address.Text,
            };

            db.EmployeesTBL.Add(a);

            UpdateProgram();

            //graphic set
            tb_A_Name.Text = "";
            tb_A_Phone.Text = "";
            tb_A_Email.Text = "";
            tb_A_Address.Text = "";
            }
            catch
            {
                ErrorWin w = new ErrorWin("adding fail.");
            }
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            //locate the object using id + remove from db
            int id = int.Parse((cb_D_Employees.Text).Remove((cb_D_Employees.Text).IndexOf(".")));
            db.EmployeesTBL.Remove(db.EmployeesTBL.Where(i => i.Id == id).First());
            
            UpdateProgram();
        }
        
        private void UpdateChooseEmployee(object sender, RoutedEventArgs e)
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

            int id = int.Parse((cb_U_Employees.Text).Remove((cb_U_Employees.Text).IndexOf(".")));
            EmployeesTBL em = db.EmployeesTBL.Where(i => i.Id == id).First();

            tb_U_Name.Text = em.Name;
            tb_U_Phone.Text = em.Phone;
            tb_U_Email.Text = em.Email;
            tb_U_Address.Text = em.Address;
        }

        private void UpdateEmployee(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse((cb_U_Employees.Text).Remove((cb_U_Employees.Text).IndexOf(".")));
                EmployeesTBL em = db.EmployeesTBL.Where(i => i.Id == id).First();
                em.Name = tb_U_Name.Text;
                em.Phone = tb_U_Phone.Text;
                em.Email = tb_U_Email.Text;
                em.Address = tb_U_Address.Text;
                
                //graphic set
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

                UpdateProgram();
            }
            catch
            {
                ErrorWin w = new ErrorWin("update fail.");
            }
        }

        public List<EmployeesTBL> getEmployees()
        {
            return db.EmployeesTBL.ToList<EmployeesTBL>();
        }

        public List<string> getEmployeesIdsAndNames()
        {
            return db.EmployeesTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        //updating db and evry wpf object that use 
        public void UpdateProgram()
        {
            db.SaveChanges();
            dg_Employees.ItemsSource = getEmployees();
            cb_D_Employees.ItemsSource = getEmployeesIdsAndNames();
            cb_U_Employees.ItemsSource = getEmployeesIdsAndNames();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }
    }
}
