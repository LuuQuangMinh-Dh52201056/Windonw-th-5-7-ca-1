using System.Text; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebThu5Ca4.Models;

namespace WebThu5Ca4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            qlhvContext db = new qlhvContext();
            DgvMonHoc.ItemsSource = db.Monhocs.ToList();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            Monhoc monhoc = gridMonhoc.DataContext as Monhoc;
            if (string.IsNullOrEmpty(monhoc.Msmh) == true)
            {
                MessageBox.Show("Bạn Chưa Nhập môn học !");
                return;
            }
            if (string.IsNullOrEmpty(monhoc.Tenmh) == true)
            {
                MessageBox.Show("Bạn chưa nhập tên môn học !");
                return;
            }
            if (monhoc.Sotiet.HasValue == false)
            {
                MessageBox.Show("Bạn chưa nhập số tiết !");
                return;
            }

            try
            {
                qlhvContext db = new qlhvContext();
                db.Monhocs.Add(monhoc);
                db.SaveChanges();

                DgvMonHoc.ItemsSource = db.Monhocs.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bị lỗi khi thêm môn! " + ex.Message);
                return;
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            Monhoc monhoc = gridMonhoc.DataContext as Monhoc;
            qlhvContext db = new qlhvContext();
            Monhoc mh = db.Monhocs.Find(monhoc.Msmh);
            if (mh != null)
            {
                mh.Tenmh = monhoc.Tenmh;
                mh.Sotiet = monhoc.Sotiet;
                db.SaveChanges();

                DgvMonHoc.ItemsSource = db.Monhocs.ToList();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            Monhoc monhoc = DgvMonHoc.SelectedItem as Monhoc;
            if (monhoc == null)
            {
                return;
            }

            MessageBoxResult ok = MessageBox.Show("Bạn có muốn xóa môn này không?","Thông Báo ",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
            if (ok != MessageBoxResult.OK)
            {
                return;
            }

            qlhvContext db = new qlhvContext();
            Monhoc mh = db.Monhocs.Find(monhoc.Msmh);
            if (mh != null)
            {
                try
                {
                    db.Monhocs.Remove(mh);
                    db.SaveChanges();

                    DgvMonHoc.ItemsSource = db.Monhocs.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bị lỗi khi xóa môn học! " + ex.Message);
                    return;
                }
            }
        }

        private void DgvMonHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Monhoc monhoc = DgvMonHoc.SelectedItem as Monhoc;
            if (monhoc == null)
            {
                return;
            }

            gridMonhoc.DataContext = new Monhoc
            {
                Msmh = monhoc.Msmh,
                Tenmh = monhoc.Tenmh,
                Sotiet = monhoc.Sotiet,
            };
        }
    }
}