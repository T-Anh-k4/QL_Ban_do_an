using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace QLBH.Models;

public partial class Khachhang
{
    public int MaKh { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập tên khách hàng.")]
    public string? TenKh { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ.")]
    public string? DiaChi { get; set; }
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Sai định dạng số điện thoại")]
    [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại.")]
    public string? Sdt { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập giới tính")]
    public string? GioiTinh { get; set; }

    public virtual ICollection<Hoadonban> Hoadonbans { get; set; } = new List<Hoadonban>();
}
