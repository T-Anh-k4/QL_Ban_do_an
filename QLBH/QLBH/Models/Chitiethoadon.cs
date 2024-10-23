using System;
using System.Collections.Generic;

namespace QLBH.Models;

public partial class Chitiethoadon
{
    public int SoHdb { get; set; }

    public int MaChiTietSp { get; set; }

    public int? SoLuong { get; set; }

    public virtual Chitietmonan MaChiTietSpNavigation { get; set; } = null!;

    public virtual Hoadonban SoHdbNavigation { get; set; } = null!;
}
