using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AZEntity> AZEntities { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=DESKTOP-A7ASUKJ;Database=mydb;Trusted_Connection=True;MultipleActiveResultSets=True");
    }
}
