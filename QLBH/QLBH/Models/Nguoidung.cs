using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLBH.Models;

public partial class Nguoidung
{
    public int Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Tên phải có từ 4 kí tự trở lên")]
    public string? TaiKhoan { get; set; }
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu từ 8 ký tự trở lên")]
    [Required]
    public string? MatKhau { get; set; }

    public string? Loai { get; set; }

    [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
    [RegularExpression(@"^[\w\.-]+@gmail\.com$", ErrorMessage = "Email phải có đuôi gmail")]
    public string Email { get; set; } = null!;
}
