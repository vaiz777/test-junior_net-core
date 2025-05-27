using Microsoft.EntityFrameworkCore;
using ExportApp.Models;

namespace ExportApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Siswa> Siswa { get; set; }
    }
}
