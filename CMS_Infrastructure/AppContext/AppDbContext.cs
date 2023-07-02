using CMS_Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Infrastructure.AppContext
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<PhatTu> PhatTu { get; set; }
        public DbSet<DaoTrang> DaoTrang { get; set; }
        public DbSet<DonDangKy> DonDangKy { get; set; }
        public DbSet<KieuThanhVien> KieuThanhVien { get; set; }
        public DbSet<PhatTuDaoTrang> PhatTuDaoTrang { get; set; }
        public DbSet<Chua> Chua { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DUONGPC\\DUONG;Integrated Security=true;Initial Catalog=QuanLyPhatTu;MultipleActiveResultSets=True;");
            //optionsBuilder.UseSqlServer($"Server=ADMIN-PC; Database=PhatTu; Trusted_connection = True; TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DonDangKy>(entity =>
            {
                entity.HasOne(e => e.PhatTu).WithMany().HasForeignKey("Id").OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
