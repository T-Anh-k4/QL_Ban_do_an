using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLBH.Models;

public partial class Monan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaMonAn { get; set; }

    public int MaLoai { get; set; }

    public string? TenHh { get; set; }

    public decimal? DonGiaBan { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual Loaimonan MaLoaiNavigation { get; set; } = null!;
}
