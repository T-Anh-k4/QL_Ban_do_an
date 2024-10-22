using System;
using System.Collections.Generic;

namespace QLBH.Models;

public partial class Chitietmonan
{
    public int MaChiTietSp { get; set; }

    public int? MaMonAn { get; set; }

    public int? SoLuong { get; set; }

    public string? AnhDaiDien { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual Monan? MaMonAnNavigation { get; set; }
}
