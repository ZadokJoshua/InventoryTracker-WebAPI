using InventoryTracker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.WebAPI.DbContexts
{
    public class InventoryTrackerDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public InventoryTrackerDbContext(DbContextOptions<InventoryTrackerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    Id = 1,
                    Name = "Xbox One",
                    Description= "",
                    SerialNumber = "AXB124AXY",
                    Value = 399.00M
                },
                new Item()
                {
                    Id = 2,
                    Name = "Samsung TV",
                    Description = "",
                    SerialNumber = "S40AZBDE4",
                    Value = 599.99M
                });
        }
    }
}
