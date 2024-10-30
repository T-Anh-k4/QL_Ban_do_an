using System;
using System.Collections.Generic;

namespace QLBH.Models;

public partial class Hoadonban
{
    public int SoHdb { get; set; }

    public int? MaKh { get; set; }

    public DateTime? NgayDatHang { get; set; }

    public decimal? TongTien { get; set; }

    public string? TrangThaiTt { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual Khachhang? MaKhNavigation { get; set; }
}
