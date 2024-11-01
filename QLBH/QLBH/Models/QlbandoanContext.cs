using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLBH.Models;

public partial class QlbandoanContext : DbContext
{
    public QlbandoanContext()
    {
    }

    public QlbandoanContext(DbContextOptions<QlbandoanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitiethoadon> Chitiethoadons { get; set; }

    public virtual DbSet<Chitietmonan> Chitietmonans { get; set; }

    public virtual DbSet<Hoadonban> Hoadonbans { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loaimonan> Loaimonans { get; set; }

    public virtual DbSet<Monan> Monans { get; set; }

    public virtual DbSet<Nguoidung> Nguoidungs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NAUDA\\SQLEXPRESS;Initial Catalog=QLBANDOAN;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitiethoadon>(entity =>
        {
            entity.HasKey(e => new { e.SoHdb, e.MaChiTietSp });

            entity.ToTable("CHITIETHOADON");

            entity.Property(e => e.SoHdb).HasColumnName("SoHDB");
            entity.Property(e => e.MaChiTietSp).HasColumnName("MaChiTietSP");

            entity.HasOne(d => d.MaChiTietSpNavigation).WithMany(p => p.Chitiethoadons)
                .HasForeignKey(d => d.MaChiTietSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHOADON_CHITIETMONAN");

            entity.HasOne(d => d.SoHdbNavigation).WithMany(p => p.Chitiethoadons)
                .HasForeignKey(d => d.SoHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHOADON_HOADONBAN");
        });

        modelBuilder.Entity<Chitietmonan>(entity =>
        {
            entity.HasKey(e => e.MaChiTietSp);

            entity.ToTable("CHITIETMONAN");

            entity.Property(e => e.MaChiTietSp)
                .ValueGeneratedNever()
                .HasColumnName("MaChiTietSP");
            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ThanhTien).HasColumnType("money");

            entity.HasOne(d => d.MaMonAnNavigation).WithMany(p => p.Chitietmonans)
                .HasForeignKey(d => d.MaMonAn)
                .HasConstraintName("FK_CHITIETMONAN_MONAN");
        });

        modelBuilder.Entity<Hoadonban>(entity =>
        {
            entity.HasKey(e => e.SoHdb);

            entity.ToTable("HOADONBAN");

            entity.Property(e => e.SoHdb)
                .ValueGeneratedNever()
                .HasColumnName("SoHDB");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.NgayDatHang).HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThaiTt)
                .HasMaxLength(50)
                .HasColumnName("TrangThaiTT");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Hoadonbans)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_HOADONBAN_KHACHHANG");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.MaKh)
                .ValueGeneratedNever()
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.GioiTinh).HasMaxLength(5);
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("SDT");
            entity.Property(e => e.TenKh)
                .HasMaxLength(100)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<Loaimonan>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("LOAIMONAN");

            entity.Property(e => e.MaLoai).ValueGeneratedNever();
            entity.Property(e => e.TenLoai).HasMaxLength(100);
        });

        modelBuilder.Entity<Monan>(entity =>
        {
            entity.HasKey(e => e.MaMonAn);

            entity.ToTable("MONAN");

            entity.Property(e => e.MaMonAn).ValueGeneratedNever();
            entity.Property(e => e.Anh)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DonGiaBan).HasColumnType("money");
            entity.Property(e => e.TenHh)
                .HasMaxLength(100)
                .HasColumnName("TenHH");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.Monans)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_MONAN_LOAIMONAN");
        });

        modelBuilder.Entity<Nguoidung>(entity =>
        {
            entity.ToTable("NGUOIDUNG");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Loai).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TaiKhoan).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
