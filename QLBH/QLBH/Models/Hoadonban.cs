using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLBH.Models;

public partial class Hoadonban
{
    public int SoHdb { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập mã khách hàng.")]
    public int? MaKh { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập ngày đặt hàng.")]
    public DateTime? NgayDatHang { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập tổng tiền.")]
    public decimal? TongTien { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập trạng thái.")]
    public string? TrangThaiTt { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual Khachhang? MaKhNavigation { get; set; }
}
