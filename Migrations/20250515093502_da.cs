using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quanlykytucxa.Migrations
{
    public partial class da : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DichvuKTX",
                columns: table => new
                {
                    maDV = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    giadichvu = table.Column<int>(type: "int", nullable: true),
                    ghichu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DichvuKT__7A3EF41167699EAD", x => x.maDV);
                });

            migrationBuilder.CreateTable(
                name: "Khu",
                columns: table => new
                {
                    maKhu = table.Column<int>(type: "int", nullable: false),
                    TenKhu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Khu__26DF73D23333FDB8", x => x.maKhu);
                });

            migrationBuilder.CreateTable(
                name: "loaiPhong",
                columns: table => new
                {
                    maloai = table.Column<int>(type: "int", nullable: false),
                    tenloai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    soluongSV = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__loaiPhon__734B3AEAF30A0FD0", x => x.maloai);
                });

            migrationBuilder.CreateTable(
                name: "NhanVienQL",
                columns: table => new
                {
                    maNV = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Hoten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SDT = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    CCCD = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__7A3EC7D5A547C89A", x => x.maNV);
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenSV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    SDT = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    CCCD = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phong",
                columns: table => new
                {
                    maPhong = table.Column<int>(type: "int", nullable: false),
                    maKhu = table.Column<int>(type: "int", nullable: false),
                    maloai = table.Column<int>(type: "int", nullable: false),
                    soluongSV = table.Column<int>(type: "int", nullable: false),
                    soluongdk = table.Column<int>(type: "int", nullable: true),
                    tienphong = table.Column<int>(type: "int", nullable: true),
                    mota = table.Column<string>(type: "text", nullable: true),
                    loaiphong = table.Column<int>(type: "int", nullable: true),
                    trangthai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Phong__4CD55E1095BC4458", x => x.maPhong);
                    table.ForeignKey(
                        name: "FK__Phong__maKhu__34C8D9D1",
                        column: x => x.maKhu,
                        principalTable: "Khu",
                        principalColumn: "maKhu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Phong__maloai__35BCFE0A",
                        column: x => x.maloai,
                        principalTable: "loaiPhong",
                        principalColumn: "maloai",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_SinhVien_UserId",
                        column: x => x.UserId,
                        principalTable: "SinhVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_SinhVien_UserId",
                        column: x => x.UserId,
                        principalTable: "SinhVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_SinhVien_UserId",
                        column: x => x.UserId,
                        principalTable: "SinhVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_SinhVien_UserId",
                        column: x => x.UserId,
                        principalTable: "SinhVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    maThongBao = table.Column<int>(type: "int", nullable: false),
                    SinhVienId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayDang = table.Column<DateTime>(type: "datetime", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThongBao__657CA53970739E5D", x => x.maThongBao);
                    table.ForeignKey(
                        name: "FK_ThongBao_SinhVien",
                        column: x => x.SinhVienId,
                        principalTable: "SinhVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DangKyKTX",
                columns: table => new
                {
                    maDK = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    SinhVienId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    maPhong = table.Column<int>(type: "int", nullable: true),
                    transId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NgayDangKy = table.Column<DateTime>(type: "date", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "date", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValueSql: "('Đang ?')"),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ngaythanhtoan = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DangKyKT__7A3EF40A8D942706", x => x.maDK);
                    table.ForeignKey(
                        name: "FK__DangKyKTX__maPho__3B75D760",
                        column: x => x.maPhong,
                        principalTable: "Phong",
                        principalColumn: "maPhong",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DangKyKtx_SinhVien",
                        column: x => x.SinhVienId,
                        principalTable: "SinhVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Diennuoc",
                columns: table => new
                {
                    maDN = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: false),
                    transId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    maPhong = table.Column<int>(type: "int", nullable: true),
                    sodien = table.Column<int>(type: "int", nullable: true),
                    sonuoc = table.Column<int>(type: "int", nullable: true),
                    giadien = table.Column<int>(type: "int", nullable: true),
                    gianuoc = table.Column<int>(type: "int", nullable: true),
                    ngaytao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Diennuoc__7A3EF4097267577A", x => x.maDN);
                    table.ForeignKey(
                        name: "FK__Diennuoc__maPhon__440B1D61",
                        column: x => x.maPhong,
                        principalTable: "Phong",
                        principalColumn: "maPhong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YeuCauSuaChua",
                columns: table => new
                {
                    maYeuCau = table.Column<int>(type: "int", nullable: false),
                    SinhVienId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    maPhong = table.Column<int>(type: "int", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValueSql: "('Đang th?c hi?n')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__YeuCauSu__765F6DD6460F277A", x => x.maYeuCau);
                    table.ForeignKey(
                        name: "FK__YeuCauSua__maPho__4D94879B",
                        column: x => x.maPhong,
                        principalTable: "Phong",
                        principalColumn: "maPhong",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__YeuCauSuaC__maSV__4CA06362",
                        column: x => x.SinhVienId,
                        principalTable: "SinhVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChitietDKdichvu",
                columns: table => new
                {
                    maDK = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    maDv = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    ngaydangki = table.Column<DateTime>(type: "date", nullable: true),
                    trangthai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChitietD__7D9D1B4922C61172", x => new { x.maDK, x.maDv });
                    table.ForeignKey(
                        name: "FK__ChitietDKd__maDK__403A8C7D",
                        column: x => x.maDK,
                        principalTable: "DangKyKTX",
                        principalColumn: "maDK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ChitietDKd__maDv__412EB0B6",
                        column: x => x.maDv,
                        principalTable: "DichvuKTX",
                        principalColumn: "maDV",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    maHoaDon = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    maDN = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: false),
                    giadien = table.Column<int>(type: "int", nullable: true),
                    gianuoc = table.Column<int>(type: "int", nullable: true),
                    TienD = table.Column<int>(type: "int", nullable: false),
                    TienNc = table.Column<int>(type: "int", nullable: false),
                    ngaytao = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoaDon__026B4D9A63D2BD1F", x => x.maHoaDon);
                    table.ForeignKey(
                        name: "FK__HoaDon__maDN__47DBAE45",
                        column: x => x.maDN,
                        principalTable: "Diennuoc",
                        principalColumn: "maDN",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ChitietDKdichvu_maDv",
                table: "ChitietDKdichvu",
                column: "maDv");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyKTX_maPhong",
                table: "DangKyKTX",
                column: "maPhong");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyKTX_SinhVienId",
                table: "DangKyKTX",
                column: "SinhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_Diennuoc_maPhong",
                table: "Diennuoc",
                column: "maPhong");

            migrationBuilder.CreateIndex(
                name: "UQ__HoaDon__7A3EF408168A331D",
                table: "HoaDon",
                column: "maDN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__55F68FC0409A49CD",
                table: "NhanVienQL",
                column: "TenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__A955A0AAB86250BB",
                table: "NhanVienQL",
                column: "CCCD",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__AB6E616409AE3F0C",
                table: "NhanVienQL",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__CA1930A5BBF122E6",
                table: "NhanVienQL",
                column: "SDT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phong_maKhu",
                table: "Phong",
                column: "maKhu");

            migrationBuilder.CreateIndex(
                name: "IX_Phong_maloai",
                table: "Phong",
                column: "maloai");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "SinhVien",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UQ__SinhVien__A955A0AA840616F4",
                table: "SinhVien",
                column: "CCCD",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__SinhVien__CA1930A577F0B174",
                table: "SinhVien",
                column: "SDT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "SinhVien",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_SinhVienId",
                table: "ThongBao",
                column: "SinhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauSuaChua_maPhong",
                table: "YeuCauSuaChua",
                column: "maPhong");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauSuaChua_SinhVienId",
                table: "YeuCauSuaChua",
                column: "SinhVienId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChitietDKdichvu");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "NhanVienQL");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "YeuCauSuaChua");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DangKyKTX");

            migrationBuilder.DropTable(
                name: "DichvuKTX");

            migrationBuilder.DropTable(
                name: "Diennuoc");

            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "Phong");

            migrationBuilder.DropTable(
                name: "Khu");

            migrationBuilder.DropTable(
                name: "loaiPhong");
        }
    }
}
