using Centhora_Hotels.Models;
using Microsoft.EntityFrameworkCore;

namespace Centhora_Hotels.DB_Context
{
    public class BeachWoodDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public BeachWoodDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //connecting with PostgreSQL database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PgSQL-DbConnection"));
        }


        //Model entity configurations using fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //preventing table names being pluralized on creation
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Booking>().ToTable("Booking");
            modelBuilder.Entity<RoomType>().ToTable("RoomType");
            modelBuilder.Entity<RoomPrice>().ToTable("RoomPrice");

            // Define the primary key of the User table
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            // Define the primary key of the Booking table
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.BookingId);

            // Define the primary key of the RoomType table
            modelBuilder.Entity<RoomType>()
                .HasKey(rt => rt.RoomTypeId);

            // Define a foreign key constraint for one-to-many relationship between User and Booking tables
            modelBuilder.Entity<Booking>()
                .HasOne(u => u.User)
                .WithMany(b => b.Bookings)
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define a foreign key constraint for one-to-many relationship between RoomType and Booking tables
            modelBuilder.Entity<Booking>()
                .HasOne(rt => rt.RoomType)
                .WithMany(b => b.Bookings)
                .HasForeignKey(fr => fr.RoomTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the primary key of the RoomPrice table (RoomPrice table's rimary key is the primary key of RoomType table)
            modelBuilder.Entity<RoomPrice>()
                .HasKey(rtp => rtp.RoomTypeId);

            // Define one-to-one relationship between RoomType and RoomPrice tables
            modelBuilder.Entity<RoomType>()
                .HasOne(rp => rp.RoomPrice)
                .WithOne(rt => rt.RoomType)
                .OnDelete(DeleteBehavior.Cascade);

        }

        // Entities
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<RoomType> Rooms { get; set; }
        public DbSet<RoomPrice> Prices { get; set; }
    }
}
