using System;
using System.Collections.Generic;

namespace QLBH.Models;

public partial class Loaimonan
{
    public int MaLoai { get; set; }

    public string? TenLoai { get; set; }

    public virtual ICollection<Monan> Monans { get; set; } = new List<Monan>();
}
