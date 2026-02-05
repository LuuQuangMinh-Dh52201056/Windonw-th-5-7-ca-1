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
            CChitiethoadon ct = new CChitiethoadon()
            {
                Mahang = cthd.Mahang,
                Soluong = cthd.Soluong,
                MahangNavigation = cthd.MahangNavigation,
                Dongia = cthd.MahangNavigation.Dongia
            };

            hoadon.Chitiethoadons.Add(ct);

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
            e.CanExecute = true;
        }
    }
}
