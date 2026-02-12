using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace WpfApp1
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            UI.WindowHoaDon windowHoaDon = new UI.WindowHoaDon();
            windowHoaDon.Show();
        }

        private void mnuHangHoa_Click(object sender, RoutedEventArgs e)
        {
            UI.WindowHangHoa windowHangHoa = new UI.WindowHangHoa();
            windowHangHoa.Show();
        }
    }
}