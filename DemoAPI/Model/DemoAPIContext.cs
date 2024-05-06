using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Model
{
    public class DemoAPIContext:DbContext

    {
        public DemoAPIContext(DbContextOptions<DemoAPIContext> options) : base(options) 
        {

        }
            public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>().HasData(
        //        new Category { CID = 1, CName = "IT", CCapacity = 4 },
        //        new Category { CID = 2, CName = "HR", CCapacity = 2 },
        //         new Category { CID = 3, CName = "Accounts", CCapacity = 3 }
        //);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
    }
    
