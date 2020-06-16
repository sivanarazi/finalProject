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
    /// Interaction logic for OrdersFromSuppliersWin.xaml
    /// </summary>
    public partial class OrdersFromSuppliersWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        private MainWindow m;
        public OrdersFromSuppliersWin(MainWindow m)
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

        private void AddOrderFromSupplier(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dt = new DateTime(int.Parse(tb_A_Year.Text), int.Parse(tb_A_Month.Text), int.Parse(tb_A_Day.Text));

                //turn type to int
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

                //create new object - detais
                OrdersFromSuppliersDetailsTBL b = new OrdersFromSuppliersDetailsTBL
                {
                    Album = int.Parse((cb_A_Album.Text).Remove((cb_A_Album.Text).IndexOf("."))),
                    Type = t,
                    Amount = int.Parse(tb_A_Amount.Text),
                    Date = dt,
                    Cost = int.Parse(tb_A_Cost.Text),
                };

                db.OrdersFromSuppliersDetailsTBL.Add(b);

                db.SaveChanges();

                //create new object
                OrdersFromSuppliersTBL a = new OrdersFromSuppliersTBL
                {
                    Supplier = int.Parse((cb_A_Supplier.Text).Remove((cb_A_Supplier.Text).IndexOf("."))),
                    Employee = int.Parse((cb_A_Employee.Text).Remove((cb_A_Employee.Text).IndexOf("."))),
                    Details = b.Id,
                };

                db.OrdersFromSuppliersTBL.Add(a);

                db.SaveChanges();

                //check if the products exsist - if true, chant the product amount / if false, create new product
                if (db.StockTBL.Where(i => i.Album == b.Album && i.Type == b.Type).Any())
                {
                    (db.StockTBL.Where(i => i.Album == b.Album && i.Type == b.Type).First()).Amount += b.Amount;
                }
                else
                {
                    StockTBL s = new StockTBL
                    {
                        Album = b.Album,
                        Type = b.Type,
                        Amount = b.Amount,
                    };
                    db.StockTBL.Add(s);
                }
                
                UpdateProgram();

                //graphic set
                cb_A_Album.Text = "";
                cb_A_Type.Text = "";
                tb_A_Amount.Text = "";
                tb_A_Year.Text = "";
                tb_A_Month.Text = "";
                tb_A_Day.Text = "";
                tb_A_Cost.Text = "";
                cb_A_Employee.Text = "";
                cb_A_Supplier.Text = "";
            }
            catch
            {
                ErrorWin w = new ErrorWin("add order fail.");
            }
        }

        private void DeleteOrderFromSupplier(object sender, RoutedEventArgs e)
        {
            //locate the object using id
            int id = int.Parse(cb_D_OrdersFromSuppliers.Text);            
            db.OrdersFromSuppliersTBL.Remove(db.OrdersFromSuppliersTBL.Where(i => i.Id == id).First());
            
            UpdateProgram();
        }

        private void UpdateChooseOrderFromSupplier(object sender, RoutedEventArgs e)
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
                tb_U_Cost.Visibility = Visibility.Visible;
                cb_U_Employee.Visibility = Visibility.Visible;
                cb_U_Supplier.Visibility = Visibility.Visible;
                tbl_U_Album.Visibility = Visibility.Visible;
                tbl_U_Type.Visibility = Visibility.Visible;
                tbl_U_Amount.Visibility = Visibility.Visible;
                tbl_U_Year.Visibility = Visibility.Visible;
                tbl_U_Month.Visibility = Visibility.Visible;
                tbl_U_Day.Visibility = Visibility.Visible;
                tbl_U_Cost.Visibility = Visibility.Visible;
                tbl_U_Employee.Visibility = Visibility.Visible;
                tbl_U_Supplier.Visibility = Visibility.Visible;
                b_U.Visibility = Visibility.Visible;

                //locate the object using id
                int id = int.Parse(cb_U_OrdersFromSuppliers.Text);
                OrdersFromSuppliersTBL cs = db.OrdersFromSuppliersTBL.Where(i => i.Id == id).First();
                OrdersFromSuppliersDetailsTBL csD = db.OrdersFromSuppliersDetailsTBL.Where(i => i.Id == cs.Details).First();
                EmployeesTBL em = db.EmployeesTBL.Where(i => i.Id == cs.Employee).First();
                SuppliersTBL s = db.SuppliersTBL.Where(i => i.Id == cs.Supplier).First();
                AlbumsTBL a = db.AlbumsTBL.Where(i => i.Id == csD.Album).First();

                //write the current object variables
                cb_U_Album.Text = csD.Album + ". " + a.Name;
                cb_U_Type.Text = csD.Type.ToString();
                tb_U_Amount.Text = csD.Amount.ToString();
                tb_U_Year.Text = csD.Date.Year.ToString();
                tb_U_Month.Text = csD.Date.Month.ToString();
                tb_U_Day.Text = csD.Date.Day.ToString();
                tb_U_Cost.Text = csD.Cost.ToString();
                cb_U_Employee.Text = cs.Employee + ". " + em.Name;
                cb_U_Supplier.Text = cs.Supplier + ". " + s.Name;
            }
            catch
            {
                ErrorWin w = new ErrorWin("choose update order fail.");
            }
        }

        private void UpdateOrderFromSupplier(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dt = new DateTime(int.Parse(tb_U_Year.Text), int.Parse(tb_U_Month.Text), int.Parse(tb_U_Day.Text));

                //locate the object using id
                int id = int.Parse(cb_U_OrdersFromSuppliers.Text);
                OrdersFromSuppliersTBL cs = db.OrdersFromSuppliersTBL.Where(i => i.Id == id).First();
                OrdersFromSuppliersDetailsTBL csD = db.OrdersFromSuppliersDetailsTBL.Where(i => i.Id == cs.Details).First();

                //album type to int
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
                csD.Cost = int.Parse(tb_U_Cost.Text);
                cs.Supplier = int.Parse((cb_U_Supplier.Text).Remove((cb_U_Supplier.Text).IndexOf(".")));
                cs.Employee = int.Parse((cb_U_Employee.Text).Remove((cb_U_Employee.Text).IndexOf(".")));

                //graphic set
                cb_U_Album.Visibility = Visibility.Hidden;
                cb_U_Type.Visibility = Visibility.Hidden;
                tb_U_Amount.Visibility = Visibility.Hidden;
                tb_U_Year.Visibility = Visibility.Hidden;
                tb_U_Month.Visibility = Visibility.Hidden;
                tb_U_Day.Visibility = Visibility.Hidden;
                tb_U_Cost.Visibility = Visibility.Hidden;
                cb_U_Employee.Visibility = Visibility.Hidden;
                cb_U_Supplier.Visibility = Visibility.Hidden;
                tbl_U_Album.Visibility = Visibility.Hidden;
                tbl_U_Type.Visibility = Visibility.Hidden;
                tbl_U_Amount.Visibility = Visibility.Hidden;
                tbl_U_Year.Visibility = Visibility.Hidden;
                tbl_U_Month.Visibility = Visibility.Hidden;
                tbl_U_Day.Visibility = Visibility.Hidden;
                tbl_U_Cost.Visibility = Visibility.Hidden;
                tbl_U_Employee.Visibility = Visibility.Hidden;
                tbl_U_Supplier.Visibility = Visibility.Hidden;
                b_U.Visibility = Visibility.Hidden;

                cb_U_Album.Text = "";
                cb_U_Type.Text = "";
                tb_U_Amount.Text = "";
                tb_U_Year.Text = "";
                tb_U_Month.Text = "";
                tb_U_Day.Text = "";
                tb_U_Cost.Text = "";
                cb_U_Employee.Text = "";
                cb_U_Supplier.Text = "";

                UpdateProgram();
            }
            catch
            {
                ErrorWin w = new ErrorWin("choose update order fail.");
            }
        }

        public List<OrdersFromSuppliersTBL> getOrdersFromSuppliers()
        {
            return db.OrdersFromSuppliersTBL.ToList<OrdersFromSuppliersTBL>();
        }

        public List<int> getOrdersFromSuppliersIds()
        {
            return db.OrdersFromSuppliersTBL.Select(dr => dr.Id).ToList();
        }

        public List<string> getEmployeesIdsAndNames()
        {
            return db.EmployeesTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public List<string> getAlbumsIdsAndNames()
        {
            return db.AlbumsTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public List<string> getSuppliersIdsAndNames()
        {
            return db.SuppliersTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        //updating db and evry wpf object that use db info
        public void UpdateProgram()
        {
            db.SaveChanges();
            dg_OrdersFromSuppliers.ItemsSource = getOrdersFromSuppliers();
            cb_A_Employee.ItemsSource = getEmployeesIdsAndNames();
            cb_U_Employee.ItemsSource = getEmployeesIdsAndNames();
            cb_A_Album.ItemsSource = getAlbumsIdsAndNames();
            cb_U_Album.ItemsSource = getAlbumsIdsAndNames();
            cb_A_Supplier.ItemsSource = getSuppliersIdsAndNames();
            cb_U_Supplier.ItemsSource = getSuppliersIdsAndNames();
            cb_D_OrdersFromSuppliers.ItemsSource = getOrdersFromSuppliersIds();
            cb_U_OrdersFromSuppliers.ItemsSource = getOrdersFromSuppliersIds();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }
    }
}
