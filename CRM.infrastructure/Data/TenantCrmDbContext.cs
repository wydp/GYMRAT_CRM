using CRM.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.infrastructure.Data
{
    public class TenantCrmDbContext : DbContext
    {
        public TenantCrmDbContext(DbContextOptions<TenantCrmDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
        public DbSet<Inquiry> Inquiries => Set<Inquiry>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.ProductId);
                entity.Property(x => x.ProductCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.ProductName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.UnitPrice).HasPrecision(18, 2);
                entity.HasIndex(x => x.ProductCode).IsUnique();
            });

            builder.Entity<Customer>(entity =>
            {
                entity.HasKey(x => x.CustomerId);
                entity.Property(x => x.CustomerCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.CustomerName).HasMaxLength(200).IsRequired();
                entity.HasIndex(x => x.CustomerCode).IsUnique();
            });

            builder.Entity<MembershipPlan>(entity =>
            {
                entity.HasKey(x => x.MembershipPlanId);
                entity.Property(x => x.PlanCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.PlanName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Price).HasPrecision(18, 2);
                entity.HasIndex(x => x.PlanCode).IsUnique();
            });

            builder.Entity<Inquiry>(entity =>
            {
                entity.HasKey(x => x.InquiryId);
                entity.Property(x => x.Subject).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Message).HasMaxLength(2000).IsRequired();
                entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);

                entity.HasOne(x => x.Customer)
                      .WithMany()
                      .HasForeignKey(x => x.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.IsActive).HasDefaultValue(true);
            });

            builder.Entity<Feedback>(entity =>
            {
                entity.HasKey(x => x.FeedbackId);
                entity.Property(x => x.Type).HasConversion<string>().HasMaxLength(20);   // ← added, was missing
                entity.Property(x => x.Message).HasMaxLength(2000).IsRequired();          // ← "Subject" → removed, Feedback has no Subject
                entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);

                entity.HasOne(x => x.Customer)
                      .WithMany()
                      .HasForeignKey(x => x.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.IsActive).HasDefaultValue(true);
            });
        }
    }
}