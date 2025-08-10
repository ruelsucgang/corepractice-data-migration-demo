using Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Persistence;

public class CorePracticeDbContext : DbContext
{
    public CorePracticeDbContext(DbContextOptions<CorePracticeDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Treatment> Treatments => Set<Treatment>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceLineItem> InvoiceLineItems => Set<InvoiceLineItem>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Patient>(e =>
        {
            e.HasKey(x => x.PatientId);
            e.Property(x => x.PatientIdentifier).IsRequired().HasMaxLength(50);
            e.Property(x => x.Email).HasMaxLength(256);
            e.Property(x => x.State).HasMaxLength(6);
            e.Property(x => x.Postcode).HasMaxLength(6);
        });

        b.Entity<Treatment>(e =>
        {
            e.HasKey(x => x.TreatmentId);
            e.Property(x => x.TreatmentIdentifier).IsRequired().HasMaxLength(50);
            e.Property(x => x.Description).IsRequired().HasMaxLength(512);
            e.Property(x => x.ItemCode).IsRequired().HasMaxLength(10);
            e.Property(x => x.Fee).HasColumnType("decimal(19,4)");
        });

        b.Entity<Invoice>(e =>
        {
            e.HasKey(x => x.InvoiceId);
            e.Property(x => x.InvoiceIdentifier).IsRequired().HasMaxLength(50);
            e.Property(x => x.InvoiceNo).IsRequired();
            e.Property(x => x.Total).HasColumnType("decimal(19,4)");
            e.Property(x => x.Paid).HasColumnType("decimal(19,4)");
            e.Property(x => x.Discount).HasColumnType("decimal(19,4)");
        });

        b.Entity<InvoiceLineItem>(e =>
        {
            e.HasKey(x => x.InvoiceLineItemId);
            e.Property(x => x.InvoiceLineItemIdentifier).IsRequired().HasMaxLength(50);
            e.Property(x => x.Description).IsRequired().HasMaxLength(512);
            e.Property(x => x.ItemCode).IsRequired().HasMaxLength(10);
            e.Property(x => x.UnitAmount).HasColumnType("decimal(19,4)");
            e.Property(x => x.LineAmount).HasColumnType("decimal(19,4)");
        });
    }
}
