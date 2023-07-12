using Microsoft.EntityFrameworkCore;
using WebApplication48.Models;

namespace WebApplication48
{
    public class EntityDatabase : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserModel> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }
    }
}
