using System;
using System.Collections.Generic;

namespace QLBH.Models;

public partial class Monan
{
    public int MaMonAn { get; set; }

    public int MaLoai { get; set; }

    public string? TenHh { get; set; }

    public decimal? DonGiaBan { get; set; }

    public byte[]? Anh { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual Loaimonan MaLoaiNavigation { get; set; } = null!;
}
