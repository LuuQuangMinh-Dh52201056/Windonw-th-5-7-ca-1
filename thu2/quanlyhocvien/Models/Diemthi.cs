using System;
using System.Collections.Generic;

namespace quanlyhocvien.Models
{
    public partial class Diemthi
    {
        public string Mshv { get; set; } = null!;
        public string Msmh { get; set; } = null!;
        public string? Diem { get; set; }

        public virtual Lylich MshvNavigation { get; set; } = null!;
        public virtual Monhoc MsmhNavigation { get; set; } = null!;
    }
}
