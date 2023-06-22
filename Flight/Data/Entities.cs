using Flight.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flight.Data
{
    public class Entities : DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();

        public DbSet<FlightInfo> Flights => Set<FlightInfo>();

        public Entities(DbContextOptions<Entities> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email);

            modelBuilder.Entity<FlightInfo>().Property(p => p.RemainingNumberOfSeats).IsConcurrencyToken();

            modelBuilder.Entity<FlightInfo>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<FlightInfo>().OwnsOne(f => f.Arrival);
            modelBuilder.Entity<FlightInfo>().OwnsMany(f => f.Bookings);

        }

    }
}
