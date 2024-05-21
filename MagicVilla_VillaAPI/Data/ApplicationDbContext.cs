using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Villa> Villas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    id = 1,
                    name = "Royal Villa",
                    details = "Villa xin xo",
                    imageUrl = "https://plus.unsplash.com/premium_photo-1715876234545-88509db72eb3?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    amenity = "",
                    occupancy = 1,
                    rate = 200,
                    sqft = 200,
                    createDate = DateTime.Now
                },
                new Villa()
                {
                    id = 2,
                    name = "Luxury Villa",
                    details = "Villa nha giao",
                    imageUrl = "https://plus.unsplash.com/premium_photo-1715876234545-88509db72eb3?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    amenity = "",
                    occupancy = 2,
                    rate = 100,
                    sqft = 100,
                    createDate = DateTime.Now
                },
                new Villa()
                {
                    id = 3,
                    name = "Normal Villa",
                    details = "Villa standard",
                    imageUrl = "https://plus.unsplash.com/premium_photo-1715876234545-88509db72eb3?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    amenity = "",
                    occupancy = 3,
                    rate = 300,
                    sqft = 300,
                    createDate = DateTime.Now
                }
                );
        }
    }
}
