using WpfApp1.Models;

namespace WpfApp1.MyModels
{
    public class CChitiethoadon
    {
        public CChitiethoadon()
        {
            Soluong = 1;
        }

        public string Sohd { get; set; }
        public string Mahang { get; set; }
        public double? Dongia { get; set; }
        public int? Soluong { get; set; }
        public string Tenhang { get { return MahangNavigation.Tenhang; } }
        public string Dvt { get { return MahangNavigation.Dvt; } }
        public double Thanhtien { get { return Soluong.Value * Dongia.Value; } }

        public virtual Hanghoa MahangNavigation { get; set; }
    }
}
