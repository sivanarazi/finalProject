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
    /// Interaction logic for AddEmployeeWin.xaml
    /// </summary>
    public partial class AddEmployeeWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        public AddEmployeeWin()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string nameS = tb_name.Text;
            string phoneS = tb_phone.Text;
            string emailS = tb_email.Text;
            string addressS = tb_address.Text;
            EmployeesTBL a = new EmployeesTBL
            {
                Name = nameS,
                Phone = phoneS,
                Email = emailS,
                Address = addressS,
            };
            db.EmployeesTBL.Add(a);
            db.SaveChanges();
            EmployeesWin w = new EmployeesWin();
            w.Show();
            this.Close();
        }
    }
}
