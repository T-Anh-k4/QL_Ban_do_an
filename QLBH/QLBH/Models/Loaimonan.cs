using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLBH.Models;

public partial class Loaimonan
{
    [Required(ErrorMessage = "Xin vui lòng nhập mã loại.")]
    public int MaLoai { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập tên loại sản phẩm.")]
    public string? TenLoai { get; set; }

    public virtual ICollection<Monan> Monans { get; set; } = new List<Monan>();
}
