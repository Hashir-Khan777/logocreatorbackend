using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Graphics> Graphics { get; set; }
    }
}