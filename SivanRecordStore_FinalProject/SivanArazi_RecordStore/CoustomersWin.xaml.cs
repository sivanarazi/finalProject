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
    /// Interaction logic for CoustomersWin.xaml
    /// </summary>
    public partial class CoustomersWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        public CoustomersWin()
        {
            InitializeComponent();
            dg.ItemsSource = getCoustomers();
        }

        private void AddCoustomer(object sender, RoutedEventArgs e)
        {
            AddCoustomerWin w = new AddCoustomerWin();
            w.Show();
            this.Close();
        }

        public List<CoustomerTBL> getCoustomers()
        {
            return db.CoustomerTBL.ToList<CoustomerTBL>();
        }
    }
}
