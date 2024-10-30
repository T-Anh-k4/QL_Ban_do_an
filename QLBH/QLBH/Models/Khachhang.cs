using System;
using System.Collections.Generic;

namespace QLBH.Models;

public partial class Khachhang
{
    public int MaKh { get; set; }

    public string? TenKh { get; set; }

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public string? GioiTinh { get; set; }

    public virtual ICollection<Hoadonban> Hoadonbans { get; set; } = new List<Hoadonban>();
}
