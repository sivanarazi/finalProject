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
        public EmployeesWin()
        {
            InitializeComponent();
            UpdateProgram();
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            EmployeesTBL a = new EmployeesTBL
            {
                Name = tb_name.Text,
                Phone = tb_phone.Text,
                Email = tb_email.Text,
                Address = tb_address.Text,
            };
            db.EmployeesTBL.Add(a);
            db.SaveChanges();
            UpdateProgram();
            tb_name.Text = "";
            tb_phone.Text = "";
            tb_email.Text = "";
            tb_address.Text = "";
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((cb_DelEmployees.Text).Remove((cb_DelEmployees.Text).IndexOf(".")));
            db.EmployeesTBL.Remove(db.EmployeesTBL.Where(i => i.Id == id).First());
            db.SaveChanges();
            UpdateProgram();
        }        

        private void UpdateChooseEmployee(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateEmployee(object sender, RoutedEventArgs e)
        {
            /*int id = int.Parse((cb_UpEmployees.Text).Remove((cb_UpEmployees.Text).IndexOf(".")));
            var result = db.EmployeesTBL.SingleOrDefault(b => b.Id == id);
            if (result != null)
            {
                result.SomeValue = "Some new value";
                db.SaveChanges();
            }*/
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
            cb_DelEmployees.ItemsSource = getEmployeesIdsAndNames();
            cb_UpEmployees.ItemsSource = getEmployeesIdsAndNames();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
