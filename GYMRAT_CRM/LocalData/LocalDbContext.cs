using CRM.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.winforms.LocalData
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
        public DbSet<MembershipSale> MembershipSales => Set<MembershipSale>();
        public DbSet<OutboxEntry> OutboxEntries => Set<OutboxEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(x => x.CustomerId);
                entity.Property(x => x.CustomerCode).HasMaxLength(50);
                entity.Property(x => x.CustomerName).HasMaxLength(200);
            });

            modelBuilder.Entity<MembershipPlan>(entity =>
            {
                entity.HasKey(x => x.MembershipPlanId);
                entity.Property(x => x.PlanCode).HasMaxLength(50);
                entity.Property(x => x.PlanName).HasMaxLength(200);
            });

            modelBuilder.Entity<MembershipSale>(entity =>
            {
                entity.HasKey(x => x.MembershipSaleId);
                entity.Property(x => x.AmountPaid).HasPrecision(18, 2);
            });

            modelBuilder.Entity<OutboxEntry>(entity =>
            {
                entity.HasKey(x => x.OutboxEntryId);
                entity.Property(x => x.EntityType).HasMaxLength(100);
                entity.Property(x => x.Operation).HasMaxLength(20);
                entity.Property(x => x.Payload).HasColumnType("TEXT");
            });
        }
    }
}
