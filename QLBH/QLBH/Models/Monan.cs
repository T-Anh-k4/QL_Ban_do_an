using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLBH.Models;

public partial class Monan
{
    [Required(ErrorMessage = "Xin vui lòng nhập mã món ăn.")]
    public int MaMonAn { get; set; }
    [Required(ErrorMessage = "Mã loại món ăn không thể trống.")]
    public int? MaLoai { get; set; }
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Tên món ăn không được chứa số.")]
    [Required(ErrorMessage = "Xin vui lòng nhập tên món ăn.")]
    public string? TenHh { get; set; }
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Giá trị không hợp lệ. Nó phải là số với tối đa 2 chữ số thập phân.")]
    [Required(ErrorMessage = "Xin vui lòng nhập đơn giá bán.")]
    public decimal? DonGiaBan { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập đường dẫn của ảnh.")]
    public string? Anh { get; set; }
    public virtual ICollection<Chitietmonan> Chitietmonans { get; set; } = new List<Chitietmonan>();
    public virtual Loaimonan? MaLoaiNavigation { get; set; }
}
