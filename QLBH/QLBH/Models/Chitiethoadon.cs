using System;
using System.Collections.Generic;

namespace QLBH.Models;

public partial class Chitiethoadon
{
    public int SoHdb { get; set; }

    public int MaMonAn { get; set; }

    public int? SoLuong { get; set; }

    public virtual Monan MaMonAnNavigation { get; set; } = null!;

    public virtual Hoadonban SoHdbNavigation { get; set; } = null!;
}
