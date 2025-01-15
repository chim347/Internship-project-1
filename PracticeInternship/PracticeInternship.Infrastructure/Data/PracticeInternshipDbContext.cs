using Microsoft.EntityFrameworkCore;
using PracticeInternship.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Infrastructure.Data
{
    public class PracticeInternshipDbContext(DbContextOptions<PracticeInternshipDbContext> options) : DbContext(options)
    {
        public DbSet<DM_Don_Vi_Tinh> DM_Don_Vi_Tinh { get; set; }
        public DbSet<DM_Loai_San_Pham> DM_Loai_San_Pham { get; set; }
        public DbSet<DM_San_Pham> DM_San_Pham { get; set; }
        public DbSet<DM_NCC> DM_NCC { get; set; }
        public DbSet<DM_Kho> DM_Kho { get; set; }
        public DbSet<DM_Kho_User> DM_Kho_User { get; set; }
        public DbSet<DM_Nhap_Kho> DM_Nhap_Kho { get; set; }
        public DbSet<DM_Nhap_Kho_Raw_Data> DM_Nhap_Kho_Raw_Data { get; set; }
        public DbSet<XNK_Nhap_Kho> XNK_Nhap_Kho { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // index với IsUnique
            modelBuilder.Entity<DM_Kho_User>()
                .HasIndex(k => new { k.Ma_Dang_Nhap, k.Kho_Id })
                .IsUnique();
        }
    }
}
