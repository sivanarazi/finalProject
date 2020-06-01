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
    /// Interaction logic for AlbumsWin.xaml
    /// </summary>
    public partial class AlbumsWin : Window
    {
        DataBaseEntities db = new DataBaseEntities();
        private MainWindow m;
        public AlbumsWin(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            UpdateProgram();
        }

        private void AddAlbum(object sender, RoutedEventArgs e)
        {
            AlbumsTBL a = new AlbumsTBL
            {
                Name = tb_A_Name.Text,
                Artist = tb_A_Artist.Text,
                SongsNumber = int.Parse(tb_A_SongsNumber.Text),
                Year = int.Parse(tb_A_Year.Text),
                Picture = null,
            };
            db.AlbumsTBL.Add(a);
            db.SaveChanges();
            UpdateProgram();
            tb_A_Name.Text = "";
            tb_A_Artist.Text = "";
            tb_A_SongsNumber.Text = "";
            tb_A_Year.Text = "";
            UpdateProgram();
        }

        private void DeleteAlbum(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((cb_D_Albums.Text).Remove((cb_D_Albums.Text).IndexOf(".")));
            db.AlbumsTBL.Remove(db.AlbumsTBL.Where(i => i.Id == id).First());
            db.SaveChanges();
            UpdateProgram();
        }

        private void UpdateChooseAlbum(object sender, RoutedEventArgs e)
        {
            tb_U_Name.Visibility = Visibility.Visible;
            tb_U_Artist.Visibility = Visibility.Visible;
            tb_U_SongsNumber.Visibility = Visibility.Visible;
            tb_U_Year.Visibility = Visibility.Visible;
            tbl_U_Name.Visibility = Visibility.Visible;
            tbl_U_Artist.Visibility = Visibility.Visible;
            tbl_U_SongsNumber.Visibility = Visibility.Visible;
            tbl_U_Year.Visibility = Visibility.Visible;

            tb_U_Name.Text = "";
            tb_U_Artist.Text = "";
            tb_U_SongsNumber.Text = "";
            tb_U_Year.Text = "";
        }

        private void UpdateAlbum(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((cb_U_Albums.Text).Remove((cb_U_Albums.Text).IndexOf(".")));
            AlbumsTBL cs = db.AlbumsTBL.Where(i => i.Id == id).First();
            cs.Name = tb_U_Name.Text;
            cs.Artist = tb_U_Artist.Text;
            cs.SongsNumber = int.Parse(tb_U_SongsNumber.Text);
            cs.Year = int.Parse(tb_U_Year.Text);

            tb_U_Name.Visibility = Visibility.Hidden;
            tb_U_Artist.Visibility = Visibility.Hidden;
            tb_U_SongsNumber.Visibility = Visibility.Hidden;
            tb_U_Year.Visibility = Visibility.Hidden;
            tbl_U_Name.Visibility = Visibility.Hidden;
            tbl_U_Artist.Visibility = Visibility.Hidden;
            tbl_U_SongsNumber.Visibility = Visibility.Hidden;
            tbl_U_Year.Visibility = Visibility.Hidden;

            tb_U_Name.Text = "";
            tb_U_Artist.Text = "";
            tb_U_SongsNumber.Text = "";
            tb_U_Year.Text = "";

            db.SaveChanges();
            UpdateProgram();
        }

        public List<AlbumsTBL> getAlbums()
        {
            return db.AlbumsTBL.ToList<AlbumsTBL>();
        }

        public List<string> getAlbumsIdsAndNames()
        {
            return db.AlbumsTBL.Select(dr => dr.Id + ". " + dr.Name).ToList();
        }

        public void UpdateProgram()
        {
            dg_Albums.ItemsSource = getAlbums();
            cb_D_Albums.ItemsSource = getAlbumsIdsAndNames();
            cb_U_Albums.ItemsSource = getAlbumsIdsAndNames();
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            this.m.Show();
            this.Close();
        }
    }
}
