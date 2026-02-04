using quanlyhocvien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyhocvien.MyModels
{
    internal class CHocVien
    {
        public string Mshv { get; set; } = null!;
        public string? Tenhv { get; set; }
        public string? Ngaysinh { get; set; }
        public string? Phai { get; set; }
        public string? Malop { get; set; }

        public string? TenLop   { get; set; }

        public static CHocVien Chuyendoi(Lylich x)
        {
            return new CHocVien
            {
                Mshv = x.Mshv,
                Tenhv = x.Tenhv,
                Ngaysinh = x.Ngaysinh.Value.ToShortDateString(),
                Phai = (x.Phai.Value ? "Nam" : "Nữ"),
                Malop = x.Malop,
                TenLop = x.MalopNavigation.Tenlop
            };
        }

    }
}
