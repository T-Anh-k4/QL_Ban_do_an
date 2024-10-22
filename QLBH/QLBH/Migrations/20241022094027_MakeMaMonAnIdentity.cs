using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLBH.Migrations
{
    /// <inheritdoc />
    public partial class MakeMaMonAnIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KHACHHANG",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    TenKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHACHHANG", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "LOAIMONAN",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOAIMONAN", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "NGUOIDUNG",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    TaiKhoan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Loai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGUOIDUNG", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HOADONBAN",
                columns: table => new
                {
                    SoHDB = table.Column<int>(type: "int", nullable: false),
                    MaKH = table.Column<int>(type: "int", nullable: true),
                    NgayDatHang = table.Column<DateTime>(type: "datetime", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrangThaiTT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOADONBAN", x => x.SoHDB);
                    table.ForeignKey(
                        name: "FK_HOADONBAN_KHACHHANG",
                        column: x => x.MaKH,
                        principalTable: "KHACHHANG",
                        principalColumn: "MaKH");
                });

            migrationBuilder.CreateTable(
                name: "MONAN",
                columns: table => new
                {
                    MaMonAn = table.Column<int>(type: "int", nullable: false),
                    MaLoai = table.Column<int>(type: "int", nullable: true),
                    TenHH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DonGiaBan = table.Column<decimal>(type: "money", nullable: true),
                    Anh = table.Column<string>(type: "char(100)", unicode: false, fixedLength: true, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MONAN", x => x.MaMonAn);
                    table.ForeignKey(
                        name: "FK_MONAN_LOAIMONAN",
                        column: x => x.MaLoai,
                        principalTable: "LOAIMONAN",
                        principalColumn: "MaLoai");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETMONAN",
                columns: table => new
                {
                    MaChiTietSP = table.Column<int>(type: "int", nullable: false),
                    MaMonAn = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "char(100)", unicode: false, fixedLength: true, maxLength: 100, nullable: true),
                    ThanhTien = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETMONAN", x => x.MaChiTietSP);
                    table.ForeignKey(
                        name: "FK_CHITIETMONAN_MONAN",
                        column: x => x.MaMonAn,
                        principalTable: "MONAN",
                        principalColumn: "MaMonAn");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETHOADON",
                columns: table => new
                {
                    SoHDB = table.Column<int>(type: "int", nullable: false),
                    MaChiTietSP = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETHOADON", x => new { x.SoHDB, x.MaChiTietSP });
                    table.ForeignKey(
                        name: "FK_CHITIETHOADON_CHITIETMONAN",
                        column: x => x.MaChiTietSP,
                        principalTable: "CHITIETMONAN",
                        principalColumn: "MaChiTietSP");
                    table.ForeignKey(
                        name: "FK_CHITIETHOADON_HOADONBAN",
                        column: x => x.SoHDB,
                        principalTable: "HOADONBAN",
                        principalColumn: "SoHDB");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETHOADON_MaChiTietSP",
                table: "CHITIETHOADON",
                column: "MaChiTietSP");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETMONAN_MaMonAn",
                table: "CHITIETMONAN",
                column: "MaMonAn");

            migrationBuilder.CreateIndex(
                name: "IX_HOADONBAN_MaKH",
                table: "HOADONBAN",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_MONAN_MaLoai",
                table: "MONAN",
                column: "MaLoai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHITIETHOADON");

            migrationBuilder.DropTable(
                name: "NGUOIDUNG");

            migrationBuilder.DropTable(
                name: "CHITIETMONAN");

            migrationBuilder.DropTable(
                name: "HOADONBAN");

            migrationBuilder.DropTable(
                name: "MONAN");

            migrationBuilder.DropTable(
                name: "KHACHHANG");

            migrationBuilder.DropTable(
                name: "LOAIMONAN");
        }
    }
}
