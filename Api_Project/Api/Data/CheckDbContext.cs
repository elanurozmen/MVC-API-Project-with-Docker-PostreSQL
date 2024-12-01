using Microsoft.EntityFrameworkCore;
using Api_Project.Models;

    public class CheckDbContext : DbContext
    {
        public CheckDbContext (DbContextOptions<CheckDbContext> options)
            : base(options)
        {
        }

        public DbSet<CheckItem> CheckItem { get; set; } = default!;
    }
