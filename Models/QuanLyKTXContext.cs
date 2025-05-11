using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Quanlykytucxa
{
    public partial class QuanLyKTXContext : DbContext
    {
        public QuanLyKTXContext()
        {
        }

        public QuanLyKTXContext(DbContextOptions<QuanLyKTXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DangKyKtx> DangKyKtxes { get; set; }
        public virtual DbSet<Diennuoc> Diennuocs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<Khu> Khus { get; set; }
        public virtual DbSet<NhanVienQl> NhanVienQls { get; set; }
        public virtual DbSet<Phong> Phongs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }
        public virtual DbSet<YeuCauSuaChua> YeuCauSuaChuas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnections");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<DangKyKtx>(entity =>
            {
                entity.HasKey(e => e.MaDk)
                    .HasName("PK__DangKyKT__7A3EF40AF1C8B903");

                entity.ToTable("DangKyKTX");

                entity.Property(e => e.MaDk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDK")
                    .IsFixedLength(true);

                entity.Property(e => e.GhiChu).HasMaxLength(255);

                entity.Property(e => e.MaPhong).HasColumnName("maPhong");

                entity.Property(e => e.MaSv)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maSV")
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDangKy).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.Ngaythanhtoan)
                    .HasColumnType("date")
                    .HasColumnName("ngaythanhtoan");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Đang ?')");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.DangKyKtxes)
                    .HasForeignKey(d => d.MaPhong)
                    .HasConstraintName("FK__DangKyKTX__maPho__38996AB5");

                entity.HasOne(d => d.MaSvNavigation)
                    .WithMany(p => p.DangKyKtxes)
                    .HasForeignKey(d => d.MaSv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DangKyKTX__maSV__37A5467C");
            });

            modelBuilder.Entity<Diennuoc>(entity =>
            {
                entity.HasKey(e => e.MaDn)
                    .HasName("PK__Diennuoc__7A3EF409E689BB28");

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

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.Diennuocs)
                    .HasForeignKey(d => d.MaPhong)
                    .HasConstraintName("FK__Diennuoc__maPhon__3B75D760");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__HoaDon__026B4D9AC2EF533A");

                entity.ToTable("HoaDon");

                entity.HasIndex(e => e.MaDn, "UQ__HoaDon__7A3EF40878B18700")
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
                    .HasConstraintName("FK__HoaDon__maDN__3F466844");
            });

            modelBuilder.Entity<Khu>(entity =>
            {
                entity.HasKey(e => e.MaKhu)
                    .HasName("PK__Khu__26DF73D258F6673E");

                entity.ToTable("Khu");

                entity.Property(e => e.MaKhu)
                    .ValueGeneratedNever()
                    .HasColumnName("maKhu");

                entity.Property(e => e.TenKhu)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVienQl>(entity =>
            {
                entity.HasKey(e => e.MaNv)
                    .HasName("PK__NhanVien__7A3EC7D5FE9F4FA2");

                entity.ToTable("NhanVienQL");

                entity.HasIndex(e => e.TenDangNhap, "UQ__NhanVien__55F68FC067EC0689")
                    .IsUnique();

                entity.HasIndex(e => e.Cccd, "UQ__NhanVien__A955A0AAB1ADC210")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__NhanVien__AB6E6164075A8DFF")
                    .IsUnique();

                entity.HasIndex(e => e.Sdt, "UQ__NhanVien__CA1930A5B8A5899F")
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
                    .HasName("PK__Phong__4CD55E108BF88089");

                entity.ToTable("Phong");

                entity.Property(e => e.MaPhong)
                    .ValueGeneratedNever()
                    .HasColumnName("maPhong");

                entity.Property(e => e.Loaiphong).HasColumnName("loaiphong");

                entity.Property(e => e.MaKhu).HasColumnName("maKhu");

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
                    .HasConstraintName("FK__Phong__maKhu__32E0915F");
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.MaSv)
                    .HasName("PK__SinhVien__7A227A64CB21D8FE");

                entity.ToTable("SinhVien");

                entity.HasIndex(e => e.EmailSv, "UQ__SinhVien__87356E204C64D5BD")
                    .IsUnique();

                entity.HasIndex(e => e.Cccd, "UQ__SinhVien__A955A0AA27FE9A84")
                    .IsUnique();

                entity.HasIndex(e => e.Sdt, "UQ__SinhVien__CA1930A51BB76F5D")
                    .IsUnique();

                entity.Property(e => e.MaSv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maSV")
                    .IsFixedLength(true);

                entity.Property(e => e.Cccd)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CCCD");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EmailSv)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emailSV");

                entity.Property(e => e.GioiTinh)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Khoa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Lop)
                    .IsRequired()
                    .HasMaxLength(10);

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

                entity.Property(e => e.TenSv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TenSV");
            });

            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => e.MaThongBao)
                    .HasName("PK__ThongBao__657CA5398CF3F874");

                entity.ToTable("ThongBao");

                entity.Property(e => e.MaThongBao)
                    .ValueGeneratedNever()
                    .HasColumnName("maThongBao");

                entity.Property(e => e.MaSv)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maSV")
                    .IsFixedLength(true);

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.NoiDung)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TieuDe)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaSvNavigation)
                    .WithMany(p => p.ThongBaos)
                    .HasForeignKey(d => d.MaSv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThongBao__maSV__47DBAE45");
            });

            modelBuilder.Entity<YeuCauSuaChua>(entity =>
            {
                entity.HasKey(e => e.MaYeuCau)
                    .HasName("PK__YeuCauSu__765F6DD6C05EFF4B");

                entity.ToTable("YeuCauSuaChua");

                entity.Property(e => e.MaYeuCau)
                    .ValueGeneratedNever()
                    .HasColumnName("maYeuCau");

                entity.Property(e => e.Ghichu).HasMaxLength(100);

                entity.Property(e => e.MaPhong).HasColumnName("maPhong");

                entity.Property(e => e.MaSv)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maSV")
                    .IsFixedLength(true);

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Đang th?c hi?n')");

                entity.HasOne(d => d.MaPhongNavigation)
                    .WithMany(p => p.YeuCauSuaChuas)
                    .HasForeignKey(d => d.MaPhong)
                    .HasConstraintName("FK__YeuCauSua__maPho__44FF419A");

                entity.HasOne(d => d.MaSvNavigation)
                    .WithMany(p => p.YeuCauSuaChuas)
                    .HasForeignKey(d => d.MaSv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__YeuCauSuaC__maSV__440B1D61");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
