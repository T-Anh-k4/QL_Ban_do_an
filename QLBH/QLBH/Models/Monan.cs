using System;
using System.Collections.Generic;

namespace QLBH.Models;

public partial class Monan
{
    public int MaMonAn { get; set; }

    public int? MaLoai { get; set; }

    public string? TenHh { get; set; }

    public decimal? DonGiaBan { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<Chitietmonan> Chitietmonans { get; set; } = new List<Chitietmonan>();

    public virtual Loaimonan? MaLoaiNavigation { get; set; }
}
