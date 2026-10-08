using Microsoft.EntityFrameworkCore;
using StudentRecordsApi.Models;

namespace StudentRecordsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Roll number must be unique
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.RollNumber)
                .IsUnique();
        }
    }
}
