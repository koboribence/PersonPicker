using Microsoft.EntityFrameworkCore;
using PersonPicker.Models;

namespace PersonPicker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; } 
    }
}
