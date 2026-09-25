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

        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
        public DbSet<Inquiry> Inquiries => Set<Inquiry>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();
        public DbSet<MembershipSale> MembershipSales => Set<MembershipSale>();
        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<Promotion> Promotions => Set<Promotion>();
        public DbSet<PromoCode> PromoCodes => Set<PromoCode>();
        public DbSet<RetentionAction> RetentionActions => Set<RetentionAction>();
        public DbSet<Lead> Leads => Set<Lead>();
        public DbSet<Attendance> Attendances => Set<Attendance>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Branch>(entity =>
            {
                entity.HasKey(x => x.BranchId);
                entity.Property(x => x.BranchCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.BranchName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Address).HasMaxLength(500);
                entity.Property(x => x.ContactNumber).HasMaxLength(50);
                entity.HasIndex(x => x.BranchCode).IsUnique();
            });

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

            builder.Entity<MembershipSale>(entity =>
            {
                entity.HasKey(x => x.MembershipSaleId);
                entity.Property(x => x.AmountPaid).HasPrecision(18, 2);
                entity.Property(x => x.CancellationReason).HasMaxLength(500);
                entity.Property(x => x.IsCancelled).HasDefaultValue(false);

                entity.HasOne(x => x.Customer)
                      .WithMany()
                      .HasForeignKey(x => x.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.MembershipPlan)
                      .WithMany()
                      .HasForeignKey(x => x.MembershipPlanId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.IsActive).HasDefaultValue(true);
            });

            builder.Entity<Campaign>(entity =>
            {
                entity.HasKey(x => x.CampaignId);
                entity.Property(x => x.CampaignCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.CampaignName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(2000);
                entity.Property(x => x.Reason).HasMaxLength(1000);
                entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
                entity.HasIndex(x => x.CampaignCode).IsUnique();
                entity.Property(x => x.IsActive).HasDefaultValue(true);
            });

            builder.Entity<Promotion>(entity =>
            {
                entity.HasKey(x => x.PromotionId);
                entity.Property(x => x.PromotionCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.PromotionName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(2000);
                entity.Property(x => x.Reason).HasMaxLength(1000);
                entity.Property(x => x.DiscountType).HasConversion<string>().HasMaxLength(20);
                entity.Property(x => x.DiscountValue).HasPrecision(18, 2);
                entity.HasIndex(x => x.PromotionCode).IsUnique();
                entity.Property(x => x.IsActive).HasDefaultValue(true);
            });

            builder.Entity<PromoCode>(entity =>
            {
                entity.HasKey(x => x.PromoCodeId);
                entity.Property(x => x.Code).HasMaxLength(50).IsRequired();
                entity.HasIndex(x => x.Code).IsUnique();
                entity.Property(x => x.IsActive).HasDefaultValue(true);

                entity.HasOne(x => x.Promotion)
                      .WithMany()
                      .HasForeignKey(x => x.PromotionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<RetentionAction>(entity =>
            {
                entity.HasKey(x => x.RetentionActionId);
                entity.Property(x => x.Notes).HasMaxLength(2000);
                entity.Property(x => x.ActionType).HasConversion<string>().HasMaxLength(30);
                entity.Property(x => x.Outcome).HasConversion<string>().HasMaxLength(20);

                entity.HasOne(x => x.Customer)
                      .WithMany()
                      .HasForeignKey(x => x.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Lead>(entity =>
            {
                entity.HasKey(x => x.LeadId);
                entity.Property(x => x.LeadCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.ContactNumber).HasMaxLength(50);
                entity.Property(x => x.EmailAddress).HasMaxLength(200);
                entity.Property(x => x.Address).HasMaxLength(500);
                entity.Property(x => x.Notes).HasMaxLength(2000);
                entity.Property(x => x.Source).HasConversion<string>().HasMaxLength(30);
                entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
                entity.HasIndex(x => x.LeadCode).IsUnique();
                entity.Property(x => x.IsActive).HasDefaultValue(true);

                entity.HasOne(x => x.ConvertedCustomer)
                      .WithMany()
                      .HasForeignKey(x => x.ConvertedCustomerId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired(false);
            });

            builder.Entity<Attendance>(entity =>
            {
                entity.HasKey(x => x.AttendanceId);
                entity.Property(x => x.Notes).HasMaxLength(500);
                entity.Property(x => x.IsActive).HasDefaultValue(true);

                entity.HasOne(x => x.Customer)
                      .WithMany()
                      .HasForeignKey(x => x.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Index for querying a customer's attendance history (most common query)
                entity.HasIndex(x => new { x.CustomerId, x.CheckInTime });
            });
        }
    }
}