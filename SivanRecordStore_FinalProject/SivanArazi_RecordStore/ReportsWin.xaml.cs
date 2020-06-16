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
using LiveCharts;
using LiveCharts.Wpf;

namespace SivanArazi_RecordStore
{
    /// <summary>
    /// Interaction logic for ReportsWin.xaml
    /// </summary>
    public partial class ReportsWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        private MainWindow m;
        public ReportsWin(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            try
            {
                IncomesOutcomes();
            }
            catch
            {
                ErrorWin w = new ErrorWin("Incomes Outcomes Report fail.");
            }
            try
            {
                OutcomesDetails();
            }
            catch
            {
                ErrorWin w = new ErrorWin("Outcomes Details Report fail.");
            }
        }

        public void IncomesOutcomes()
        {
            //calcuklate every income to the store (frome the sales)
            int i1 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 1).Sum(i => i.Cost);
            int i2 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 2).Sum(i => i.Cost);
            int i3 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 3).Sum(i => i.Cost);
            int i4 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 4).Sum(i => i.Cost);
            int i5 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 5).Sum(i => i.Cost);
            int i6 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 6).Sum(i => i.Cost);
            int i7 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 7).Sum(i => i.Cost);
            int i8 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 8).Sum(i => i.Cost);
            int i9 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 9).Sum(i => i.Cost);
            int i10 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 10).Sum(i => i.Cost);
            int i11 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 11).Sum(i => i.Cost);
            int i12 = db.DealsDetailsTBL.ToList().FindAll(i => i.Date.Month == 12).Sum(i => i.Cost);


            int o1 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 1).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 1).Sum(i => i.Cost);
            int o2 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 2).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 2).Sum(i => i.Cost);
            int o3 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 3).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 3).Sum(i => i.Cost);
            int o4 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 4).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 4).Sum(i => i.Cost);
            int o5 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 5).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 5).Sum(i => i.Cost);
            int o6 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 6).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 6).Sum(i => i.Cost);
            int o7 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 7).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 7).Sum(i => i.Cost);
            int o8 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 8).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 8).Sum(i => i.Cost);
            int o9 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 9).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 9).Sum(i => i.Cost);
            int o10 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 10).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 10).Sum(i => i.Cost);
            int o11 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 11).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 11).Sum(i => i.Cost);
            int o12 = db.OrdersFromSuppliersDetailsTBL.ToList().FindAll(i => i.Date.Month == 12).Sum(i => i.Cost) + db.ExpensesTBL.ToList().FindAll(i => i.Date.Month == 12).Sum(i => i.Cost);

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Incomes",
                    Values = new ChartValues<int> { i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12 }
                },
                new LineSeries
                {
                    Title = "Outcomes",
                    Values = new ChartValues<int> { o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12 }
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            YFormatter = value => value.ToString("C");
            DataContext = this;
        }

        public void OutcomesDetails()
        {
            int supPay = db.OrdersFromSuppliersDetailsTBL.ToList().Sum(i => i.Cost);
            int regExp = db.ExpensesTBL.ToList().Sum(i => i.Cost);

            Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            pie.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Regular Expenses",
                    Values = new ChartValues<int> { regExp },
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Suppliers orders Payment",
                    Values = new ChartValues<int> { supPay },
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
            };

            pie.LegendLocation = LegendLocation.Bottom;
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }

        public SeriesCollection SeriesCollection { get; set; }

        public string[] Labels { get; set; }

        public Func<double, string> YFormatter { get; set; }
    }
}
