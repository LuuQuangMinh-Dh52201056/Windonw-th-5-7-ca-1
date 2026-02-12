using System.Windows;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.MyModels;

namespace WpfApp1.UI
{
    /// <summary>
    /// Interaction logic for WindowHoaDon.xaml
    /// </summary>
    public partial class WindowHoaDon : Window
    {
        private CHoadon hoadon = new CHoadon();
        public static RoutedUICommand lenhlaphoadon = new RoutedUICommand();

        public WindowHoaDon()
        {
            InitializeComponent();
        }

        private void loadHoadon()
        {
            Models.hoadonContext hoadonContext = new Models.hoadonContext();
            List<Hoadon> hoadons = hoadonContext.Hoadons.ToList();
            foreach (Hoadon hd in hoadons)
            {
                hd.Chitiethoadons = hoadonContext.Chitiethoadons.Where(t => t.Sohd == hd.Sohd).ToList();
                foreach (Chitiethoadon cthd in hd.Chitiethoadons)
                {
                    cthd.MahangNavigation = hoadonContext.Hanghoas.Find(cthd.Mahang);
                }
            }
            dgHoaDon.ItemsSource = hoadons.Select(hd => CHoadon.chuyendoi(hd)).ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadHoadon();

            hoadonContext hoadonContext = new hoadonContext();
            cmb_MaHang.ItemsSource = hoadonContext.Hanghoas.ToList();
        }

        private void btnChonHangHoa_Click(object sender, RoutedEventArgs e)
        {
            CChitiethoadon cthd = gridChitiethoadon.DataContext as CChitiethoadon;

            CChitiethoadon temp = hoadon.Chitiethoadons.FirstOrDefault(t => t.Mahang == cthd.Mahang);

            if (temp == null)
            {
                CChitiethoadon ct = new CChitiethoadon()
                {
                    Mahang = cthd.Mahang,
                    Soluong = cthd.Soluong,
                    MahangNavigation = cthd.MahangNavigation,
                    Dongia = cthd.MahangNavigation.Dongia
                };

                hoadon.Chitiethoadons.Add(ct);
            }
            else
            {
                temp.Soluong += cthd.Soluong;
            }

            dgCTHD.ItemsSource = hoadon.Chitiethoadons.ToList();
        }

        private void cmd_Laphoadon_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CHoadon x = gridHoadon.DataContext as CHoadon;
            Hoadon a = new Hoadon
            {
                Sohd = x.Sohd,
                Ngaylaphd = x.Ngaylaphd,
                Tenkh = x.Tenkh,
            };

            foreach (CChitiethoadon c in hoadon.Chitiethoadons)
            {
                Chitiethoadon b = new Chitiethoadon
                {
                    Sohd = c.Sohd,
                    Mahang = c.Mahang,
                    Soluong = c.Soluong,
                    Dongia = c.Dongia
                };
                a.Chitiethoadons.Add(b);
            }

            hoadonContext hoadonContext = new hoadonContext();
            hoadonContext.Hoadons.Add(a);
            hoadonContext.SaveChanges();

            loadHoadon();
            hoadon = new CHoadon();
            dgCTHD.ItemsSource = hoadon.Chitiethoadons.ToList();
        }

        private void cmd_Laphoadon_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CHoadon x = gridHoadon.DataContext as CHoadon;
            if (string.IsNullOrEmpty(x.Sohd) || string.IsNullOrEmpty(x.Tenkh) || x.Ngaylaphd.HasValue == false || hoadon.Chitiethoadons.Count == 0)
            {
                e.CanExecute = false;
                return;
            }
            if (hoadon.Chitiethoadons.Any(t => t.Soluong <= 0))
            {
                e.CanExecute = false;
                return;
            }

            hoadonContext hoadonContext = new hoadonContext();
            if (hoadonContext.Hoadons.Find(x.Sohd) != null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = true;
        }

        private void btnXoaCTHD_Click(object sender, RoutedEventArgs e)
        {
            CChitiethoadon x = dgCTHD.SelectedItem as CChitiethoadon;
            if (x == null)
            {
                return;
            }

            CChitiethoadon ct = hoadon.Chitiethoadons.Where(t => t.Mahang == x.Mahang).FirstOrDefault();
            if (ct != null)
            {
                hoadon.Chitiethoadons.Remove(ct);
                dgCTHD.ItemsSource = hoadon.Chitiethoadons.ToList();
            }
        }
    }
}
