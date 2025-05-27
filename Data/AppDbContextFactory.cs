using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ExportApp.Models;

namespace ExportApp.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Ganti string koneksi ini sesuai dengan appsettings.json kamu
            var connectionString = "server=localhost;port=5200;database=testjunior;user=root;password=Alp@03079411";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
