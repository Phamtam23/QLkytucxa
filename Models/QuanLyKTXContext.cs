using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Quanlykytucxa.Models
{
    public partial class QuanLyKTXContext : IdentityDbContext<SinhVien, IdentityRole, string>
    {
        public QuanLyKTXContext()
        {
        }

        public QuanLyKTXContext(DbContextOptions<QuanLyKTXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChitietDkdichvu> ChitietDkdichvus { get; set; }
        public virtual DbSet<DangKyKtx> DangKyKtxes { get; set; }
        public virtual DbSet<DichvuKtx> DichvuKtxes { get; set; }
        public virtual DbSet<Diennuoc> Diennuocs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<Khu> Khus { get; set; }
        public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public virtual DbSet<NhanVienQl> NhanVienQls { get; set; }
        public virtual DbSet<Phong> Phongs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }
        public virtual DbSet<YeuCauSuaChua> YeuCauSuaChuas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnections");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";
            var client = new IdentityRole("client");
            client.NormalizedName = "client";
            modelBuilder.Entity<IdentityRole>().HasData(admin, client);


            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");
            modelBuilder.Entity<IdentityUserLogin<string>>()
          .HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserClaim<string>>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<ChitietDkdichvu>(entity =>
            {
                entity.HasKey(e => new { e.MaDk, e.MaDv })
                    .HasName("PK__ChitietD__7D9D1B4922C61172");

                entity.ToTable("ChitietDKdichvu");

                entity.Property(e => e.MaDk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDK")
                    .IsFixedLength(true);

                entity.Property(e => e.MaDv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDv")
                    .IsFixedLength(true);

                entity.Property(e => e.Ngaydangki)
                    .HasColumnType("date")
                    .HasColumnName("ngaydangki");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.MaDkNavigation)
                    .WithMany(p => p.ChitietDkdichvus)
                    .HasForeignKey(d => d.MaDk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChitietDKd__maDK__403A8C7D");

                entity.HasOne(d => d.MaDvNavigation)
                    .WithMany(p => p.ChitietDkdichvus)
                    .HasForeignKey(d => d.MaDv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChitietDKd__maDv__412EB0B6");
            });

            modelBuilder.Entity<DangKyKtx>(entity =>
            {
                entity.HasKey(e => e.MaDk)
                    .HasName("PK__DangKyKT__7A3EF40A8D942706");

                entity.ToTable("DangKyKTX");

                entity.Property(e => e.MaDk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDK")
                    .IsFixedLength(true);

                entity.Property(e => e.GhiChu).HasMaxLength(255);

                entity.Property(e => e.MaPhong).HasColumnName("maPhong");

          
                entity.Property(e => e.NgayDangKy).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.Ngaythanhtoan)
                    .HasColumnType("date")
                    .HasColumnName("ngaythanhtoan");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Đang ?')");

                entity.Property(e => e.TransId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("transId");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.DangKyKtxes)
                    .HasForeignKey(d => d.MaPhong)
                    .HasConstraintName("FK__DangKyKTX__maPho__3B75D760");

               
            });

            modelBuilder.Entity<DichvuKtx>(entity =>
            {
                entity.HasKey(e => e.MaDv)
                    .HasName("PK__DichvuKT__7A3EF41167699EAD");

                entity.ToTable("DichvuKTX");

                entity.Property(e => e.MaDv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDV")
                    .IsFixedLength(true);

                entity.Property(e => e.Ghichu)
                    .HasColumnType("text")
                    .HasColumnName("ghichu");

                entity.Property(e => e.Giadichvu).HasColumnName("giadichvu");
            });

            modelBuilder.Entity<Diennuoc>(entity =>
            {
                entity.HasKey(e => e.MaDn)
                    .HasName("PK__Diennuoc__7A3EF4097267577A");

                entity.ToTable("Diennuoc");

                entity.Property(e => e.MaDn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("maDN")
                    .IsFixedLength(true);

                entity.Property(e => e.Giadien).HasColumnName("giadien");

                entity.Property(e => e.Gianuoc).HasColumnName("gianuoc");

                entity.Property(e => e.MaPhong).HasColumnName("maPhong");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("date")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Sodien).HasColumnName("sodien");

                entity.Property(e => e.Sonuoc).HasColumnName("sonuoc");

                entity.Property(e => e.TransId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("transId");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.Diennuocs)
                    .HasForeignKey(d => d.MaPhong)
                    .HasConstraintName("FK__Diennuoc__maPhon__440B1D61");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__HoaDon__026B4D9A63D2BD1F");

                entity.ToTable("HoaDon");

                entity.HasIndex(e => e.MaDn, "UQ__HoaDon__7A3EF408168A331D")
                    .IsUnique();

                entity.Property(e => e.MaHoaDon)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maHoaDon")
                    .IsFixedLength(true);

                entity.Property(e => e.Giadien).HasColumnName("giadien");

                entity.Property(e => e.Gianuoc).HasColumnName("gianuoc");

                entity.Property(e => e.MaDn)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("maDN")
                    .IsFixedLength(true);

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("date")
                    .HasColumnName("ngaytao");

                entity.HasOne(d => d.MaDnNavigation)
                    .WithOne(p => p.HoaDon)
                    .HasForeignKey<HoaDon>(d => d.MaDn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoaDon__maDN__47DBAE45");
            });

            modelBuilder.Entity<Khu>(entity =>
            {
                entity.HasKey(e => e.MaKhu)
                    .HasName("PK__Khu__26DF73D23333FDB8");

                entity.ToTable("Khu");

                entity.Property(e => e.MaKhu)
                    .ValueGeneratedNever()
                    .HasColumnName("maKhu");

                entity.Property(e => e.TenKhu)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LoaiPhong>(entity =>
            {
                entity.HasKey(e => e.Maloai)
                    .HasName("PK__loaiPhon__734B3AEAF30A0FD0");

                entity.ToTable("loaiPhong");

                entity.Property(e => e.Maloai)
                    .ValueGeneratedNever()
                    .HasColumnName("maloai");

                entity.Property(e => e.SoluongSv).HasColumnName("soluongSV");

                entity.Property(e => e.Tenloai)
                    .HasMaxLength(50)
                    .HasColumnName("tenloai");
            });

            modelBuilder.Entity<NhanVienQl>(entity =>
            {
                entity.HasKey(e => e.MaNv)
                    .HasName("PK__NhanVien__7A3EC7D5A547C89A");

                entity.ToTable("NhanVienQL");

                entity.HasIndex(e => e.TenDangNhap, "UQ__NhanVien__55F68FC0409A49CD")
                    .IsUnique();

                entity.HasIndex(e => e.Cccd, "UQ__NhanVien__A955A0AAB86250BB")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NhanVien__AB6E616409AE3F0C")
                    .IsUnique();

                entity.HasIndex(e => e.Sdt, "UQ__NhanVien__CA1930A5BBF122E6")
                    .IsUnique();

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maNV")
                    .IsFixedLength(true);

                entity.Property(e => e.Cccd)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CCCD");

                entity.Property(e => e.ChucVu)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.GioiTinh)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Hoten)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenDangNhap)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.HasKey(e => e.MaPhong)
                    .HasName("PK__Phong__4CD55E1095BC4458");

                entity.ToTable("Phong");

                entity.Property(e => e.MaPhong)
                    .ValueGeneratedNever()
                    .HasColumnName("maPhong");

                entity.Property(e => e.Loaiphong).HasColumnName("loaiphong");

                entity.Property(e => e.MaKhu).HasColumnName("maKhu");

                entity.Property(e => e.Maloai).HasColumnName("maloai");

                entity.Property(e => e.Mota)
                    .HasColumnType("text")
                    .HasColumnName("mota");

                entity.Property(e => e.SoluongSv).HasColumnName("soluongSV");

                entity.Property(e => e.Soluongdk).HasColumnName("soluongdk");

                entity.Property(e => e.Tienphong).HasColumnName("tienphong");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.MaKhuNavigation)
                    .WithMany(p => p.Phongs)
                    .HasForeignKey(d => d.MaKhu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Phong__maKhu__34C8D9D1");

                entity.HasOne(d => d.MaloaiNavigation)
                    .WithMany(p => p.Phongs)
                    .HasForeignKey(d => d.Maloai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Phong__maloai__35BCFE0A");
            });

            modelBuilder.Entity<DangKyKtx>()
                .HasOne(d => d.SinhVien)
                .WithMany(s => s.DangKyKtxes)
                .HasForeignKey(d => d.SinhVienId)
                .HasConstraintName("FK_DangKyKtx_SinhVien");

            modelBuilder.Entity<SinhVien>(entity =>
            {
             
                entity.ToTable("SinhVien");

                entity.HasIndex(e => e.Cccd, "UQ__SinhVien__A955A0AA840616F4")
                    .IsUnique();

                entity.HasIndex(e => e.Sdt, "UQ__SinhVien__CA1930A577F0B174")
                    .IsUnique();

                entity.Property(e => e.Cccd)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CCCD");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.GioiTinh)
                    .IsRequired()
                    .HasMaxLength(3);
                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenSv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TenSV");
            });

            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => e.MaThongBao)
                    .HasName("PK__ThongBao__657CA53970739E5D");

                entity.ToTable("ThongBao");

                entity.Property(e => e.MaThongBao)
                    .ValueGeneratedNever()
                    .HasColumnName("maThongBao");

                entity.Property(e => e.SinhVienId)              // chính là cột khóa ngoại
                    .IsRequired()
                    .HasMaxLength(450)                         // Chuẩn IdentityUser Id
                    .HasColumnName("SinhVienId");             // hoặc để tên khác tùy DB

                entity.Property(e => e.NgayDang)
                    .HasColumnType("datetime");

                entity.Property(e => e.NoiDung)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TieuDe)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.SinhVien)
                    .WithMany(p => p.ThongBaos)
                    .HasForeignKey(d => d.SinhVienId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ThongBao_SinhVien");
            });



            modelBuilder.Entity<YeuCauSuaChua>(entity =>
            {
                entity.HasKey(e => e.MaYeuCau)
                    .HasName("PK__YeuCauSu__765F6DD6460F277A");

                entity.ToTable("YeuCauSuaChua");

                entity.Property(e => e.MaYeuCau)
                    .ValueGeneratedNever()
                    .HasColumnName("maYeuCau");

                entity.Property(e => e.Ghichu).HasMaxLength(100);

                entity.Property(e => e.MaPhong).HasColumnName("maPhong");

                entity.Property(e => e.SinhVienId)              // chính là cột khóa ngoại
                        .IsRequired()
                        .HasMaxLength(450)                         // Chuẩn IdentityUser Id
                        .HasColumnName("SinhVienId");             // hoặc để tên khác tùy DB
                entity.Property(e => e.TrangThai)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Đang th?c hi?n')");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.YeuCauSuaChuas)
                    .HasForeignKey(d => d.MaPhong)
                    .HasConstraintName("FK__YeuCauSua__maPho__4D94879B");

                entity.HasOne(d => d.SinhVien)
                    .WithMany(p => p.YeuCauSuaChuas)
                    .HasForeignKey(d => d.SinhVienId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__YeuCauSuaC__maSV__4CA06362");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
