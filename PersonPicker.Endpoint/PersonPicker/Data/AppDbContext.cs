using Microsoft.EntityFrameworkCore;
using PersonPicker.Models;

namespace PersonPicker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Draw> Draws { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
