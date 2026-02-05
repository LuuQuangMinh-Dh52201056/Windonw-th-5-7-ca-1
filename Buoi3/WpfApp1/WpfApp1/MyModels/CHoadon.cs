using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.MyModels
{
    public class CHoadon
    {
        public CHoadon()
        {
            Chitiethoadons = new List<CChitiethoadon>();
        }

        public string Sohd { get; set; }
        public DateTime? Ngaylaphd { get; set; }
        public string Tenkh { get; set; }
        public double Thanhtien
        {
            get
            {
                return Chitiethoadons.Sum(t => t.Thanhtien);
            }
        }

        public virtual List<CChitiethoadon> Chitiethoadons { get; set; }

        public static CHoadon chuyendoi(Hoadon x)
        {
            CHoadon hoadon = new CHoadon()
            {
                Sohd = x.Sohd,
                Ngaylaphd = x.Ngaylaphd,
                Tenkh = x.Tenkh
            };

            foreach (Chitiethoadon c in x.Chitiethoadons)
            {
                CChitiethoadon ct = new CChitiethoadon()
                {
                    Sohd = c.Sohd,
                    Mahang = c.Mahang,
                    Soluong = c.Soluong,
                    Dongia = c.Dongia,
                    MahangNavigation = c.MahangNavigation
                };
                hoadon.Chitiethoadons.Add(ct);
            }

            return hoadon;
        }
    }
}
