using Microsoft.EntityFrameworkCore;
using StudentAPI.Model;

namespace StudentAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }

    }
}
