using Microsoft.EntityFrameworkCore;
using PhoneLookupConsoleEf.Models;

namespace PhoneLookupConsoleEf.Data;

public class AppDbContext : DbContext
{
    public DbSet<PhoneRequest> PhoneRequests => Set<PhoneRequest>();
    public DbSet<PhoneValidation> PhoneValidations => Set<PhoneValidation>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=phone_lookup.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhoneRequest>()
            .Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(30);

        modelBuilder.Entity<PhoneRequest>()
            .HasMany(x => x.Validations)
            .WithOne(x => x.PhoneRequest)
            .HasForeignKey(x => x.PhoneRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PhoneValidation>()
            .Property(x => x.CountryCode)
            .HasMaxLength(10);
    }
}
