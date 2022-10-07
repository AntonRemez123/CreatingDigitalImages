using CreatingDigitalImages.Models;
using Microsoft.EntityFrameworkCore;

namespace CreatingDigitalImages
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CDI> CDI { get; set; }
    }
}
