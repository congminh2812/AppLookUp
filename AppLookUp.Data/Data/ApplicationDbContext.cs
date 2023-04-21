using AppLookUp.Models;
using AppLookUp.Models.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppLookUp.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<TypeInfo> TypeInfos { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<Component> Components { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = this.ChangeTracker.Entries()
                .Where(s => s.State == EntityState.Added)
                .Select(s => s.Entity);

            foreach (var entry in insertedEntries)
            {
                var auditableEntity = entry as IAuditable;
                if (auditableEntity is not null)
                    auditableEntity.CreatedTime = DateTime.Now;
            }

            var updatedEntries = this.ChangeTracker.Entries()
                .Where(s => s.State == EntityState.Modified)
                .Select(s => s.Entity);

            foreach (var entry in updatedEntries)
            {
                var auditableEntity = entry as IAuditable;
                if (auditableEntity is not null)
                    auditableEntity.UpdatedTime = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
