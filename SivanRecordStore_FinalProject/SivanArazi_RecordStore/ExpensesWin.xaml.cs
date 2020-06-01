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
    /// Interaction logic for ExpensesWin.xaml
    /// </summary>
    public partial class ExpensesWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        private MainWindow m;
        public ExpensesWin(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            UpdateProgram();
        }

        private void AddExpense(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime(int.Parse(tb_A_Year.Text), int.Parse(tb_A_Month.Text), int.Parse(tb_A_Day.Text));
            ExpensesTBL a = new ExpensesTBL
            {
                Name = tb_A_Name.Text,
                Date =  dt,
                Description = tb_A_Description.Text,
                Cost = int.Parse(tb_A_Cost.Text),
                Employee = int.Parse((cb_A_Employee.Text).Remove((cb_A_Employee.Text).IndexOf("."))),
            };
            db.ExpensesTBL.Add(a);
            db.SaveChanges();
            UpdateProgram();
            tb_A_Name.Text = "";
            tb_A_Year.Text = "";
            tb_A_Month.Text = "";
            tb_A_Day.Text = "";
            tb_A_Description.Text = "";
            tb_A_Cost.Text = "";
            cb_A_Employee.Text = "";
            UpdateProgram();
        }

        private void DeleteExpense(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((cb_D_Expenses.Text).Remove((cb_D_Expenses.Text).IndexOf(".")));
            db.ExpensesTBL.Remove(db.ExpensesTBL.Where(i => i.Id == id).First());
            db.SaveChanges();
            UpdateProgram();
        }

        private void UpdateChooseExpense(object sender, RoutedEventArgs e)
        {
            tb_U_Name.Visibility = Visibility.Visible;
            tb_U_Year.Visibility = Visibility.Visible;
            tb_U_Month.Visibility = Visibility.Visible;
            tb_U_Day.Visibility = Visibility.Visible;
            tb_U_Description.Visibility = Visibility.Visible;
            tb_U_Cost.Visibility = Visibility.Visible;
            tbl_U_Name.Visibility = Visibility.Visible;
            tbl_U_Year.Visibility = Visibility.Visible;
            tbl_U_Month.Visibility = Visibility.Visible;
            tbl_U_Day.Visibility = Visibility.Visible;
            tbl_U_Description.Visibility = Visibility.Visible;
            tbl_U_Cost.Visibility = Visibility.Visible;
            b_U.Visibility = Visibility.Visible;
            cb_U_Employee.Visibility = Visibility.Visible;


            tb_U_Name.Text = "";
            tb_U_Year.Text = "";
            tb_U_Month.Text = "";
            tb_U_Day.Text = "";
            tb_U_Description.Text = "";
            tb_U_Cost.Text = "";
            cb_U_Employee.Text = "";
        }

        private void UpdateExpense(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((cb_U_Expenses.Text).Remove((cb_U_Expenses.Text).IndexOf(".")));
            ExpensesTBL cs = db.ExpensesTBL.Where(i => i.Id == id).First();
            DateTime dt = new DateTime(int.Parse(tb_U_Year.Text), int.Parse(tb_U_Month.Text), int.Parse(tb_U_Day.Text));
            cs.Name = tb_U_Name.Text;
            cs.Date = dt;
            cs.Description = tb_U_Description.Text;
            cs.Cost = int.Parse(tb_U_Cost.Text);
            cs.Employee = int.Parse((cb_U_Employee.Text).Remove((cb_U_Employee.Text).IndexOf(".")));

            tb_U_Name.Visibility = Visibility.Hidden;
            tb_U_Year.Visibility = Visibility.Hidden;
            tb_U_Month.Visibility = Visibility.Hidden;
            tb_U_Day.Visibility = Visibility.Hidden;
            tb_U_Description.Visibility = Visibility.Hidden;
            tb_U_Cost.Visibility = Visibility.Hidden;
            tbl_U_Name.Visibility = Visibility.Hidden;
            tbl_U_Year.Visibility = Visibility.Hidden;
            tbl_U_Month.Visibility = Visibility.Hidden;
            tbl_U_Day.Visibility = Visibility.Hidden;
            tbl_U_Description.Visibility = Visibility.Hidden;
            tbl_U_Cost.Visibility = Visibility.Hidden;
            b_U.Visibility = Visibility.Hidden;
            cb_U_Employee.Visibility = Visibility.Hidden;

            tb_U_Name.Text = "";
            tb_U_Year.Text = "";
            tb_U_Month.Text = "";
            tb_U_Day.Text = "";
            tb_U_Description.Text = "";
            tb_U_Cost.Text = "";
            cb_U_Employee.Text = "";
            db.SaveChanges();
            UpdateProgram();
        }

        public List<ExpensesTBL> getExpenses()
        {
            return db.ExpensesTBL.ToList<ExpensesTBL>();
        }

        public List<string> getExpensesIdsAndNames()
        {
            return db.ExpensesTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public List<string> getEmployeesIdsAndNames()
        {
            return db.EmployeesTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public void UpdateProgram()
        {
            cb_A_Employee.ItemsSource = getEmployeesIdsAndNames();
            cb_U_Employee.ItemsSource = getEmployeesIdsAndNames();
            dg_Expenses.ItemsSource = getExpenses();
            cb_D_Expenses.ItemsSource = getExpensesIdsAndNames();
            cb_U_Expenses.ItemsSource = getExpensesIdsAndNames();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }
    }
}
