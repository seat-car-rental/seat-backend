using Microsoft.EntityFrameworkCore;
using SeatAPI.Models;

namespace SeatAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Users (Owner, Renter, Customer Service, Admin)
        public DbSet<User> Users { get; set; }

        // Cars listed by Owners
        public DbSet<Car> Cars { get; set; }

        // Rentals - Managing car renting process
        public DbSet<Rental> Rentals { get; set; }

        // Notifications for Owners & Renters
        public DbSet<Notification> Notifications { get; set; }

        // Payments - To handle transactions
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure Unique Email for Users
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // One-to-Many: User (Owner) → Cars
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.OwnedCars)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: User (Renter) → Rentals
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Renter)
                .WithMany(u => u.Rentals)
                .HasForeignKey(r => r.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-One: Rental → Car (Each rental is linked to one car)
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-One: Payment → Rental
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Rental)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.RentalId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
