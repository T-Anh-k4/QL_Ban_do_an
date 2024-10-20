using System;
using System.Collections.Generic;

namespace QLBH.Models;

public partial class Nguoidung
{
    public int Id { get; set; }

    public string? TaiKhoan { get; set; }

    public string? MatKhau { get; set; }

    public string? Loai { get; set; }
}
