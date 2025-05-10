using Microsoft.EntityFrameworkCore;
using PROG_WEB_APP.Models;

namespace PROG_WEB_APP.DATA
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }


    }
}
