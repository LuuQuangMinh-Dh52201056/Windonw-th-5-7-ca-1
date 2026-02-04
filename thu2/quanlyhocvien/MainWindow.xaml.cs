using Microsoft.EntityFrameworkCore;
using quanlyhocvien.Models;
using quanlyhocvien.MyModels;
using System.Linq;
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

namespace quanlyhocvien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public static RoutedUICommand lenhThem = new RoutedUICommand();
        public static RoutedUICommand lenhXoa = new RoutedUICommand();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            quanlyhocvienContext quanlyhocvienContext = new quanlyhocvienContext();
            List<Lylich> ds = quanlyhocvienContext.Lyliches.Include(t => t.MalopNavigation).ToList();
            DgHocVien.ItemsSource = ds.Select(t => CHocVien.Chuyendoi(t)).ToList();

            cmbMaLop.ItemsSource = quanlyhocvienContext.Lops.ToList();
        }

        private void cmdThem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Lylich lylich = grdLyLich.DataContext as Lylich;
            quanlyhocvienContext quanlyhocvienContext = new quanlyhocvienContext();
            quanlyhocvienContext.Lyliches.Add(lylich);
            quanlyhocvienContext.SaveChanges();

            List<Lylich> ds = quanlyhocvienContext.Lyliches.Include(t => t.MalopNavigation).ToList();
            DgHocVien.ItemsSource = ds.Select(t => CHocVien.Chuyendoi(t)).ToList();
        }

        private void cmdThem_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Lylich lylich = grdLyLich.DataContext as Lylich;
            if (string.IsNullOrEmpty(lylich.Mshv) == true
                || string.IsNullOrEmpty(lylich.Tenhv) == true
                || string.IsNullOrEmpty(lylich.Malop) == true
                || lylich.Ngaysinh.HasValue == false
                || lylich.Phai.HasValue == false)
            {
                e.CanExecute = false;
                return;
            }

            quanlyhocvienContext quanlyhocvienContext = new quanlyhocvienContext();
            if (quanlyhocvienContext.Lyliches.Find(lylich.Mshv) != null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = true;
        }
        
        private void cmdXoa_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult ok = MessageBox.Show("Ban co that su muon xoa hoc vien nay", "Canh bao", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (ok != MessageBoxResult.OK)
            {
                return;
            }

            CHocVien cHocVien = DgHocVien.SelectedItem as CHocVien;
            quanlyhocvienContext quanlyhocvienContext = new quanlyhocvienContext();
            Lylich lylich = quanlyhocvienContext.Lyliches.Find(cHocVien.Mshv);
            if ( lylich != null)
            {
                quanlyhocvienContext.Lyliches.Remove(lylich);
                quanlyhocvienContext.SaveChanges();

                List<Lylich> ds = quanlyhocvienContext.Lyliches.Include(t => t.MalopNavigation).ToList();
                DgHocVien.ItemsSource = ds.Select(t => CHocVien.Chuyendoi(t)).ToList();
            }
        }

        private void cmdXoa_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (DgHocVien == null || DgHocVien.SelectedItem == null)
            {
                e.CanExecute = false;
                return;
            }

            CHocVien cHocVien = DgHocVien.SelectedItem as CHocVien;
            quanlyhocvienContext quanlyhocvienContext = new quanlyhocvienContext();
            if (quanlyhocvienContext.Diemthis.Count(t => t.Mshv == cHocVien.Mshv) > 0)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = true;
        }
    }
}