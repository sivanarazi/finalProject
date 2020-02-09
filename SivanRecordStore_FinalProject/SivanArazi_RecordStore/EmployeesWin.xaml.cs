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
            dg.ItemsSource = getEmployees();
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            AddEmployeeWin w = new AddEmployeeWin();
            w.Show();
            this.Close();
        }

        private void RemoveEmployee(object sender, RoutedEventArgs e)
        {

        }
        public List<EmployeesTBL> getEmployees()
        {
            return db.EmployeesTBL.ToList<EmployeesTBL>();
        }
    }
}
