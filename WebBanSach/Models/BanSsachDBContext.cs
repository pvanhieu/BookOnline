using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace WebBanSach.Models
{
    public partial class BanSsachDBContext : DbContext
    {
        public BanSsachDBContext()
            : base("name=BanSsachDBContext")
        {
        }

        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<CTDatHang> CTDatHangs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<TacGia> TacGias { get; set; }
        public virtual DbSet<VietSach> VietSaches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTDatHang>()
                .Property(e => e.Dongia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CTDatHang>()
                .Property(e => e.Thanhtien)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DonDatHang>()
                .Property(e => e.Trigia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DonDatHang>()
                .HasMany(e => e.CTDatHangs)
                .WithRequired(e => e.DonDatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.Dongia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.CTDatHangs)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.VietSaches)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TacGia>()
                .HasMany(e => e.VietSaches)
                .WithRequired(e => e.TacGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .ToTable("KhachHang");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
