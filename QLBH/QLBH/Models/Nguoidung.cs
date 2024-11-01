using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLBH.Models;

public partial class Nguoidung
{
    public int Id { get; set; }
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Độ dài tên phải từ 4 ký tự.")]
    [Required(ErrorMessage = "Xin vui lòng nhập tên tài khoản.")]
    public string? TaiKhoan { get; set; }
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Độ mật khẩu phải từ 8 ký tự.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Mật khẩu phải có ít nhất 1 ký tự viết hoa, 1 ký tự viết thường, 1 chữ số và 1 ký tự đặc biệt.")]
    [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu.")]
    public string? MatKhau { get; set; }

    public string? Loai { get; set; }
    [Required(ErrorMessage = "Xin vui lòng nhập email.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Địa chỉ email phải có đuôi @gmail.com.")]
    public string Email { get; set; } = null!;
}
