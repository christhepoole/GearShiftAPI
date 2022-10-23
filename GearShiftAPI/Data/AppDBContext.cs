using GearShiftAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GearShiftAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<UserModel> userModel { get; set; }
        public DbSet<RentalModel> rentalModel { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("user");
            modelBuilder.Entity<RentalModel>().ToTable("rentals");
        }

    }
}
