using System;
using System.Collections.Generic;

namespace quanlyhocvien.Models
{
    public partial class Lop
    {
        public Lop()
        {
            Lyliches = new HashSet<Lylich>();
        }

        public string Malop { get; set; } = null!;
        public string? Tenlop { get; set; }

        public virtual ICollection<Lylich> Lyliches { get; set; }
    }
}
