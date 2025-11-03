using KASHop.Models;
using Microsoft.EntityFrameworkCore;

namespace KASHop.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=db31100.public.databaseasp.net; Database=db31100; User Id=db31100; Password=7t%MkZ2!5=Qg; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mobiles", Description = "Mobiles" },
                new Category { Id = 2, Name = "Tablets", Description = "Tablets" },
                new Category { Id = 3, Name = "Laptops", Description = "Laptops" },
                new Category { Id = 4, Name = "Computers", Description = "Computers" },
                new Category { Id = 5, Name = "Cameras", Description = "Cameras" }


                );
        }
    }
}
