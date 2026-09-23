using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CRM.domain.Entities;

namespace CRM.infrastructure.Data
{
    public class MasterCrmDbContext : IdentityDbContext
    {
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<CompanyDatabase> CompanyDatabases => Set<CompanyDatabase>();
        public DbSet<Device> Devices { get; set; }
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        public MasterCrmDbContext(DbContextOptions<MasterCrmDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>(entity =>
            {
                entity.HasKey(x => x.CompanyId);
                entity.Property(x => x.CompanyCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.CompanyName).HasMaxLength(200).IsRequired();
                entity.HasIndex(x => x.CompanyCode).IsUnique();
            });

            builder.Entity<CompanyDatabase>(entity =>
            {
                entity.HasKey(x => x.CompanyDatabaseId);
                entity.Property(x => x.ServerName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.DatabaseName).HasMaxLength(200).IsRequired();
                entity.HasOne(x => x.Company)
                      .WithMany()
                      .HasForeignKey(x => x.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Device>(entity =>
            {
                entity.HasKey(x => x.DeviceId);
                entity.Property(x => x.DeviceCode)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(x => x.DeviceName)
                    .HasMaxLength(200)
                    .IsRequired();
                entity.HasOne(x => x.Company)
                    .WithMany(x => x.Devices)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(x => new { x.CompanyId, x.DeviceCode })
                    .IsUnique();
            });

            builder.Entity<Role>(entity =>
            {
                entity.HasKey(x => x.RoleId);
                entity.Property(x => x.RoleName).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(500);
                entity.HasIndex(x => x.RoleName).IsUnique();
            });

            builder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.UserId);
                entity.Property(x => x.Username).HasMaxLength(100).IsRequired();
                entity.Property(x => x.PasswordHash).HasMaxLength(500).IsRequired();
                entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Email).HasMaxLength(200);
                entity.HasIndex(x => x.Username).IsUnique();

                entity.HasOne(x => x.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(x => x.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Company)
                      .WithMany()
                      .HasForeignKey(x => x.CompanyId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired(false);
            });

            builder.Entity<Permission>(entity =>
            {
                entity.HasKey(x => x.PermissionId);
                entity.Property(x => x.Code).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(500);
                entity.Property(x => x.Module).HasMaxLength(50);
                entity.HasIndex(x => x.Code).IsUnique();
            });

            builder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(x => x.RolePermissionId);
                entity.HasIndex(x => new { x.RoleId, x.PermissionId }).IsUnique();

                entity.HasOne(x => x.Role)
                      .WithMany(r => r.RolePermissions)
                      .HasForeignKey(x => x.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Permission)
                      .WithMany(p => p.RolePermissions)
                      .HasForeignKey(x => x.PermissionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(x => x.AuditLogId);
                entity.Property(x => x.Action).HasMaxLength(50).IsRequired();
                entity.Property(x => x.EntityType).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Details).HasMaxLength(2000);

                entity.HasOne(x => x.User)
                      .WithMany()
                      .HasForeignKey(x => x.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}