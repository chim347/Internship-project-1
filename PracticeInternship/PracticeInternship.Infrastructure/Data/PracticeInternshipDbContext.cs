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
    }
}
