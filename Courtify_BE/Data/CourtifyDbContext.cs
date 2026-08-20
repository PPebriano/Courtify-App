using CourtifyBE.Models;
using Microsoft.EntityFrameworkCore;

namespace CourtifyBE.Data
{
    public class CourtifyDbContext : DbContext
    {
        public CourtifyDbContext(DbContextOptions<CourtifyDbContext> options) : base(options) 
        { 
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<BookingAddOns> BookingAddOns { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<CourtCategory> CourtCategories { get; set; }
        public DbSet<Courts> Courts { get; set; }
        public DbSet<EquipmentAddOns> Equipments { get; set; }
        public DbSet<PaymentReceipt> PaymentReceipts{ get; set; }
        public DbSet<Venues> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<PaymentReceipt>()
                .Property(pr => pr.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Bookings>()
                .HasOne(a => a.Admin)
                .WithMany(b => b.Bookings)
                .HasForeignKey(ai => ai.AdminId);

            modelBuilder.Entity<Courts>()
                .HasOne(v => v.Venues)
                .WithMany(c => c.Courts)
                .HasForeignKey(vi => vi.VenueId);

            modelBuilder.Entity<Courts>()
                .HasOne(cc => cc.CourtCategory)
                .WithMany(c => c.Courts)
                .HasForeignKey(ci => ci.CourtCategoryId);

            modelBuilder.Entity<Bookings>()
                .HasOne(c => c.Courts)
                .WithMany(b => b.Bookings)
                .HasForeignKey(ci => ci.CourtsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentReceipt>()
                .HasOne(b => b.Bookings)
                .WithMany(pr => pr.PaymentReceipts)
                .HasForeignKey(bi => bi.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // PIVOT MANY TO MANY
            modelBuilder.Entity<BookingAddOns>()
                .HasOne(b => b.Bookings)
                .WithMany(ba => ba.BookingAddOns)
                .HasForeignKey(bi => bi.BookingId);

            modelBuilder.Entity<BookingAddOns>()
                .HasOne(ea => ea.Equipment)
                .WithMany(ba => ba.BookingAddOns)
                //.WithMany()
                .HasForeignKey(ei => ei.EquipmentId);
        }

    }
}
