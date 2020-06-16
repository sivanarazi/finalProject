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

            //list of all albums types
            List<string> lst = new List<string>();
            lst.Add("Disk");
            lst.Add("Record");
            lst.Add("Cassette");

            cb_A_Type.ItemsSource = lst;
            cb_U_Type.ItemsSource = lst;

            UpdateProgram();
        }

        private void AddDeal(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dt = new DateTime(int.Parse(tb_A_Year.Text), int.Parse(tb_A_Month.Text), int.Parse(tb_A_Day.Text));

                //albume type to int
                int t = 0;
                if (cb_A_Type.Text == "Disk")
                {
                    t = 1;
                }
                else if (cb_A_Type.Text == "Record")
                {
                    t = 2;
                }
                else if (cb_A_Type.Text == "Cassette")
                {
                    t = 3;
                }

                //check if the stock allow the deal
                int alId = int.Parse((cb_A_Album.Text).Remove((cb_A_Album.Text).IndexOf(".")));
                int a = int.Parse(tb_A_Amount.Text);
                if ((db.StockTBL.Where(i => i.Album == alId && i.Type == t && i.Amount >= a)).Any())
                {
                    AlbumsTBL alb = db.AlbumsTBL.Where(i => i.Id == alId).First();

                    //price calculation
                    int c = alb.Price * t * int.Parse(tb_A_Amount.Text);
                    if (tb_A_Discount.Text != "" && int.Parse(tb_A_Discount.Text) > 0)
                    {
                        c = c - ((c * (int.Parse(tb_A_Discount.Text))) / 100);
                        if (c < 0)
                        {
                            c = 0;
                        }
                    }
                    Console.WriteLine(c);

                    //create new deal (+ details) object
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

                    //create new object
                    DealsTBL d = new DealsTBL
                    {
                        Customer = int.Parse((cb_A_Customer.Text).Remove((cb_A_Customer.Text).IndexOf("."))),
                        Employee = int.Parse((cb_A_Employee.Text).Remove((cb_A_Employee.Text).IndexOf("."))),
                        Details = b.Id,
                    };

                    db.DealsTBL.Add(d);

                    db.SaveChanges();

                    //update stock
                    StockTBL s = db.StockTBL.Where(i => i.Album == alId && i.Type == t).First();
                    s.Amount -= int.Parse(tb_A_Amount.Text);

                    db.SaveChanges();
                }

                //graphic set
                cb_A_Album.Text = "";
                cb_A_Type.Text = "";
                tb_A_Amount.Text = "";
                tb_A_Year.Text = "";
                tb_A_Month.Text = "";
                tb_A_Day.Text = "";
                tb_A_Discount.Text = "";
                cb_A_Employee.Text = "";
                cb_A_Customer.Text = "";

                UpdateProgram();
            }
            catch
            {
                ErrorWin w = new ErrorWin("adding fail. check text");
                w.Show();
            }
        }

        private void DeleteDeal(object sender, RoutedEventArgs e)
        {
            //locate object using id
            int id = int.Parse(cb_D_Deals.Text);            
            db.DealsTBL.Remove(db.DealsTBL.Where(i => i.Id == id).First());

            UpdateProgram();
        }

        private void UpdateChooseDeal(object sender, RoutedEventArgs e)
        {
            try
            {
                //graphic set
                cb_U_Album.Visibility = Visibility.Visible;
                cb_U_Type.Visibility = Visibility.Visible;
                tb_U_Amount.Visibility = Visibility.Visible;
                tb_U_Year.Visibility = Visibility.Visible;
                tb_U_Month.Visibility = Visibility.Visible;
                tb_U_Day.Visibility = Visibility.Visible;
                tb_U_Discount.Visibility = Visibility.Visible;
                cb_U_Employee.Visibility = Visibility.Visible;
                cb_U_Customer.Visibility = Visibility.Visible;
                tbl_U_Album.Visibility = Visibility.Visible;
                tbl_U_Type.Visibility = Visibility.Visible;
                tbl_U_Amount.Visibility = Visibility.Visible;
                tbl_U_Year.Visibility = Visibility.Visible;
                tbl_U_Month.Visibility = Visibility.Visible;
                tbl_U_Day.Visibility = Visibility.Visible;
                tbl_U_Discount.Visibility = Visibility.Visible;
                tbl_U_Employee.Visibility = Visibility.Visible;
                tbl_U_Customer.Visibility = Visibility.Visible;
                b_U.Visibility = Visibility.Visible;

                //locate the object using id
                int id = int.Parse(cb_U_Deals.Text);
                DealsTBL cs = db.DealsTBL.Where(i => i.Id == id).First();
                DealsDetailsTBL csD = db.DealsDetailsTBL.Where(i => i.Id == cs.Details).First();
                EmployeesTBL em = db.EmployeesTBL.Where(i => i.Id == cs.Employee).First();
                CustomersTBL c = db.CustomersTBL.Where(i => i.Id == cs.Customer).First();
                AlbumsTBL a = db.AlbumsTBL.Where(i => i.Id == csD.Album).First();

                //write the current object variables
                cb_U_Album.Text = csD.Album + ". " + a.Name;
                cb_U_Type.Text = csD.Type.ToString();
                tb_U_Amount.Text = csD.Amount.ToString();
                tb_U_Year.Text = csD.Date.Year.ToString();
                tb_U_Month.Text = csD.Date.Month.ToString();
                tb_U_Day.Text = csD.Date.Day.ToString();
                tb_U_Discount.Text = csD.Discount.ToString();
                cb_U_Employee.Text = cs.Employee + ". " + em.Name;
                cb_U_Customer.Text = cs.Customer + ". " + c.Name;
            }
            catch
            {
                ErrorWin w = new ErrorWin("deal update choose fail.");
            }
        }

        private void UpdateDeal(object sender, RoutedEventArgs e)
        {
            try
            { 
                DateTime dt = new DateTime(int.Parse(tb_U_Year.Text), int.Parse(tb_U_Month.Text), int.Parse(tb_U_Day.Text));

                //locate object using id
                int id = int.Parse(cb_U_Deals.Text);
                DealsTBL cs = db.DealsTBL.Where(i => i.Id == id).First();
                DealsDetailsTBL csD = db.DealsDetailsTBL.Where(i => i.Id == cs.Details).First();

                int t = 0;
                if (cb_U_Type.Text == "Disk")
                {
                    t = 1;
                }
                else if (cb_U_Type.Text == "Record")
                {
                    t = 2;
                }
                else if (cb_U_Type.Text == "Cassette")
                {
                    t = 3;
                }
                else
                {
                    t = int.Parse(cb_U_Type.Text);
                }

                //update
                csD.Album = int.Parse((cb_U_Album.Text).Remove((cb_U_Album.Text).IndexOf(".")));
                csD.Type = t;
                csD.Amount = int.Parse(tb_U_Amount.Text);
                csD.Date = dt;
                csD.Discount = int.Parse(tb_U_Discount.Text);
                cs.Customer = int.Parse((cb_U_Customer.Text).Remove((cb_U_Customer.Text).IndexOf(".")));
                cs.Employee = int.Parse((cb_U_Employee.Text).Remove((cb_U_Employee.Text).IndexOf(".")));

                int alId = int.Parse((cb_U_Album.Text).Remove((cb_U_Album.Text).IndexOf(".")));
                AlbumsTBL alb = db.AlbumsTBL.Where(i => i.Id == alId).First();

                //price calculation
                int c = alb.Price * t * int.Parse(tb_U_Amount.Text);
                if (tb_U_Discount.Text != "" && int.Parse(tb_U_Discount.Text) > 0)
                {
                    c = c - (c * (int.Parse(tb_U_Discount.Text)) / 100);
                    if (c < 0)
                    {
                        c = 0;
                    }
                }

                csD.Cost = c;

                //graphic set
                cb_U_Album.Visibility = Visibility.Hidden;
                cb_U_Type.Visibility = Visibility.Hidden;
                tb_U_Amount.Visibility = Visibility.Hidden;
                tb_U_Year.Visibility = Visibility.Hidden;
                tb_U_Month.Visibility = Visibility.Hidden;
                tb_U_Day.Visibility = Visibility.Hidden;
                tb_U_Discount.Visibility = Visibility.Hidden;
                cb_U_Employee.Visibility = Visibility.Hidden;
                cb_U_Customer.Visibility = Visibility.Hidden;
                tbl_U_Album.Visibility = Visibility.Hidden;
                tbl_U_Type.Visibility = Visibility.Hidden;
                tbl_U_Amount.Visibility = Visibility.Hidden;
                tbl_U_Year.Visibility = Visibility.Hidden;
                tbl_U_Month.Visibility = Visibility.Hidden;
                tbl_U_Day.Visibility = Visibility.Hidden;
                tbl_U_Discount.Visibility = Visibility.Hidden;
                tbl_U_Employee.Visibility = Visibility.Hidden;
                tbl_U_Customer.Visibility = Visibility.Hidden;
                b_U.Visibility = Visibility.Hidden;

                cb_U_Album.Text = "";
                cb_U_Type.Text = "";
                tb_U_Amount.Text = "";
                tb_U_Year.Text = "";
                tb_U_Month.Text = "";
                tb_U_Day.Text = "";
                tb_U_Discount.Text = "";
                cb_U_Employee.Text = "";
                cb_U_Customer.Text = "";

                UpdateProgram();
            }
            catch
            {
                ErrorWin w = new ErrorWin("update fail.");
            }
        }

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
            db.SaveChanges();
            dg_Deals.ItemsSource = getDeals();
            cb_A_Employee.ItemsSource = getEmployeesIdsAndNames();
            cb_U_Employee.ItemsSource = getEmployeesIdsAndNames();
            cb_A_Album.ItemsSource = getAlbumsIdsAndNames();
            cb_U_Album.ItemsSource = getAlbumsIdsAndNames();
            cb_A_Customer.ItemsSource = getCustomersIdsAndNames();
            cb_U_Customer.ItemsSource = getCustomersIdsAndNames();
            cb_D_Deals.ItemsSource = getDealsIds();
            cb_U_Deals.ItemsSource = getDealsIds();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }
    }
}
