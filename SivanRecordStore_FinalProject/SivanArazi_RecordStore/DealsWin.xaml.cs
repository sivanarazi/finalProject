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
    /// Interaction logic for DealsWin.xaml
    /// </summary>
    public partial class DealsWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        private MainWindow m;
        public DealsWin(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            List<string> lst = new List<string>();
            lst.Add("record");
            lst.Add("disk");
            lst.Add("cassette");
            cb_A_Type.ItemsSource = lst;
            //cb_U_Type.ItemsSource = lst;
            UpdateProgram();
        }

        private void AddDeal(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime(int.Parse(tb_A_Year.Text), int.Parse(tb_A_Month.Text), int.Parse(tb_A_Day.Text));
            int t = 0;
            if (cb_A_Type.Text == "record")
            {
                t = 1;
            }
            else if (cb_A_Type.Text == "disk")
            {
                t = 2;
            }
            else if (cb_A_Type.Text == "cassette")
            {
                t = 3;
            }
            int c = 0;
            if (tb_A_Discount.Text != "" && int.Parse(tb_A_Discount.Text) > 0)
            {
                c = (int)(((int.Parse(tb_A_Cost.Text)) * (int.Parse(tb_A_Discount.Text)) / 100));
                if (c < 0)
                {
                    c = 0;
                }
            }
            int alId = int.Parse((cb_A_Album.Text).Remove((cb_A_Album.Text).IndexOf(".")));
            int a = int.Parse(tb_A_Amount.Text);
            if ((db.StockTBL.Where(i => i.Album == alId && i.Type == t && i.Amount >= a)).Any())
            {
                DealsDetailsTBL b = new DealsDetailsTBL
                {
                    Album = int.Parse((cb_A_Album.Text).Remove((cb_A_Album.Text).IndexOf("."))),
                    Type = t,
                    Amount = int.Parse(tb_A_Amount.Text),
                    Date = dt,
                    Cost = c,
                    Discount = int.Parse(tb_A_Discount.Text),
                };
                db.DealsDetailsTBL.Add(b);
                db.SaveChanges();
                DealsTBL d = new DealsTBL
                {
                    Customer = int.Parse((cb_A_Customer.Text).Remove((cb_A_Customer.Text).IndexOf("."))),
                    Employee = int.Parse((cb_A_Employee.Text).Remove((cb_A_Employee.Text).IndexOf("."))),
                    Details = b.Id,
                };
                db.DealsTBL.Add(d);
                db.SaveChanges();
                StockTBL s = db.StockTBL.Where(i => i.Album == alId && i.Type == t).First();
                s.Amount -= int.Parse(tb_A_Amount.Text);
                db.SaveChanges();
            }
            cb_A_Album.Text = "";
            cb_A_Type.Text = "";
            tb_A_Amount.Text = "";
            tb_A_Year.Text = "";
            tb_A_Month.Text = "";
            tb_A_Day.Text = "";
            tb_A_Cost.Text = "";
            cb_A_Employee.Text = "";
            cb_A_Customer.Text = "";
            UpdateProgram();
        }

        /*private void DeleteDeal(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(cb_D_Deals.Text);            
            db.DealsTBL.Remove(db.DealsTBL.Where(i => i.Id == id).First());
            db.SaveChanges();
            UpdateProgram();
        }

        private void UpdateChooseDeal(object sender, RoutedEventArgs e)
        {
            cb_U_Album.Visibility = Visibility.Visible;
            cb_U_Type.Visibility = Visibility.Visible;
            tb_U_Amount.Visibility = Visibility.Visible;
            tb_U_Year.Visibility = Visibility.Visible;
            tb_U_Month.Visibility = Visibility.Visible;
            tb_U_Day.Visibility = Visibility.Visible;
            tb_U_Cost.Visibility = Visibility.Visible;
            cb_U_Employee.Visibility = Visibility.Visible;
            cb_U_Customer.Visibility = Visibility.Visible;
            tbl_U_Album.Visibility = Visibility.Visible;
            tbl_U_Type.Visibility = Visibility.Visible;
            tbl_U_Amount.Visibility = Visibility.Visible;
            tbl_U_Year.Visibility = Visibility.Visible;
            tbl_U_Month.Visibility = Visibility.Visible;
            tbl_U_Day.Visibility = Visibility.Visible;
            tbl_U_Cost.Visibility = Visibility.Visible;
            tbl_U_Employee.Visibility = Visibility.Visible;
            tbl_U_Customer.Visibility = Visibility.Visible;

            cb_U_Album.Text = "";
            cb_U_Type.Text = "";
            tb_U_Amount.Text = "";
            tb_U_Year.Text = "";
            tb_U_Month.Text = "";
            tb_U_Day.Text = "";
            tb_U_Cost.Text = "";
            cb_U_Employee.Text = "";
            cb_U_Customer.Text = "";
        }

        private void UpdateDeal(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime(int.Parse(tb_U_Year.Text), int.Parse(tb_U_Month.Text), int.Parse(tb_U_Day.Text));
            int id = int.Parse(cb_D_Deals.Text);
            DealsTBL cs = db.DealsTBL.Where(i => i.Id == id).First();
            DealsDetailsTBL csD = db.DealsDetailsTBL.Where(i => i.Id == cs.Details).First();
            int t = 0;
            if (cb_U_Type.Text == "record")
            {
                t = 1;
            }
            else if (cb_U_Type.Text == "disk")
            {
                t = 2;
            }
            else if (cb_U_Type.Text == "cassette")
            {
                t = 3;
            }
            csD.Album = int.Parse((cb_U_Album.Text).Remove((cb_U_Album.Text).IndexOf(".")));
            csD.Type = t;
            csD.Amount = int.Parse(tb_U_Amount.Text);
            csD.Date = dt;
            csD.Cost = int.Parse(tb_U_Cost.Text);
            cs.Customer = int.Parse((cb_U_Customer.Text).Remove((cb_U_Customer.Text).IndexOf(".")));
            cs.Employee = int.Parse((cb_U_Employee.Text).Remove((cb_U_Employee.Text).IndexOf(".")));

            cb_U_Album.Visibility = Visibility.Hidden;
            cb_U_Type.Visibility = Visibility.Hidden;
            tb_U_Amount.Visibility = Visibility.Hidden;
            tb_U_Year.Visibility = Visibility.Hidden;
            tb_U_Month.Visibility = Visibility.Hidden;
            tb_U_Day.Visibility = Visibility.Hidden;
            tb_U_Cost.Visibility = Visibility.Hidden;
            cb_U_Employee.Visibility = Visibility.Hidden;
            cb_U_Customer.Visibility = Visibility.Hidden;
            tbl_U_Album.Visibility = Visibility.Hidden;
            tbl_U_Type.Visibility = Visibility.Hidden;
            tbl_U_Amount.Visibility = Visibility.Hidden;
            tbl_U_Year.Visibility = Visibility.Hidden;
            tbl_U_Month.Visibility = Visibility.Hidden;
            tbl_U_Day.Visibility = Visibility.Hidden;
            tbl_U_Cost.Visibility = Visibility.Hidden;
            tbl_U_Employee.Visibility = Visibility.Hidden;
            tbl_U_Customer.Visibility = Visibility.Hidden;

            cb_U_Album.Text = "";
            cb_U_Type.Text = "";
            tb_U_Amount.Text = "";
            tb_U_Year.Text = "";
            tb_U_Month.Text = "";
            tb_U_Day.Text = "";
            tb_U_Cost.Text = "";
            cb_U_Employee.Text = "";
            cb_U_Customer.Text = "";
        }*/

        public List<DealsTBL> getDeals()
        {
            return db.DealsTBL.ToList<DealsTBL>();
        }

        public List<int> getDealsIds()
        {
            return db.DealsTBL.Select(dr => dr.Id).ToList();
        }

        public List<string> getEmployeesIdsAndNames()
        {
            return db.EmployeesTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public List<string> getAlbumsIdsAndNames()
        {
            return db.AlbumsTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public List<string> getCustomersIdsAndNames()
        {
            return db.CustomersTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public void UpdateProgram()
        {
            dg_Deals.ItemsSource = getDeals();
            cb_A_Employee.ItemsSource = getEmployeesIdsAndNames();
            //cb_U_Employee.ItemsSource = getEmployeesIdsAndNames();
            cb_A_Album.ItemsSource = getAlbumsIdsAndNames();
            //cb_U_Album.ItemsSource = getAlbumsIdsAndNames();
            cb_A_Customer.ItemsSource = getCustomersIdsAndNames();
            //cb_U_Customer.ItemsSource = getCustomersIdsAndNames();
            //cb_D_Deals.ItemsSource = getDealsIds();
            //cb_U_Deals.ItemsSource = getDealsIds();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }
    }
}
