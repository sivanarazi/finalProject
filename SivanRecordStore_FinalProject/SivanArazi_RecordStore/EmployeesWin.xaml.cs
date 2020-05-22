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
            EmployeesTBL a = new EmployeesTBL
            {
                Name = tb_A_Name.Text,
                Phone = tb_A_Phone.Text,
                Email = tb_A_Email.Text,
                Address = tb_A_Address.Text,
            };
            db.EmployeesTBL.Add(a);
            db.SaveChanges();
            UpdateProgram();
            tb_A_Name.Text = "";
            tb_A_Phone.Text = "";
            tb_A_Email.Text = "";
            tb_A_Address.Text = "";
            UpdateProgram();
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((cb_D_Employees.Text).Remove((cb_D_Employees.Text).IndexOf(".")));
            db.EmployeesTBL.Remove(db.EmployeesTBL.Where(i => i.Id == id).First());
            db.SaveChanges();
            UpdateProgram();
        }
        
        private void UpdateChooseEmployee(object sender, RoutedEventArgs e)
        {
            tb_U_Name.Visibility = Visibility.Visible;
            tb_U_Phone.Visibility = Visibility.Visible;
            tb_U_Email.Visibility = Visibility.Visible;
            tb_U_Address.Visibility = Visibility.Visible;
            tbl_U_Name.Visibility = Visibility.Visible;
            tbl_U_Phone.Visibility = Visibility.Visible;
            tbl_U_Email.Visibility = Visibility.Visible;
            tbl_U_Address.Visibility = Visibility.Visible;

            tb_A_Name.Text = "";
            tb_U_Phone.Text = "";
            tb_U_Email.Text = "";
            tb_U_Address.Text = "";
        }

        private void UpdateEmployee(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((cb_U_Employees.Text).Remove((cb_U_Employees.Text).IndexOf(".")));
            EmployeesTBL em = db.EmployeesTBL.Where(i => i.Id == id).First();
            em.Name = tb_U_Name.Text;
            em.Phone = tb_U_Phone.Text;
            em.Email = tb_U_Email.Text;
            em.Address = tb_U_Address.Text;

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

            db.SaveChanges();
            UpdateProgram();
        }

        public List<EmployeesTBL> getEmployees()
        {
            return db.EmployeesTBL.ToList<EmployeesTBL>();
        }

        public List<string> getEmployeesIdsAndNames()
        {
            return db.EmployeesTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public void UpdateProgram()
        {
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
