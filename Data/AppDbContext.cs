using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyProject.Entities;
using MyProject.Repository;

namespace MyProject.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ICurrentTenantService _currentTenantService;
        public string CurrentTenantId { get; set; }
        public string? TenantId { get; set; }
        private readonly ILogger<CurrentTenantService> _logger;
        public AppDbContext(ILogger<CurrentTenantService> logger, ICurrentTenantService currentTenantService, DbContextOptions<AppDbContext> options) : base(options) 
        {
            _currentTenantService = currentTenantService;
            CurrentTenantId = _currentTenantService.TenantId;
            _logger = logger;
         }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Product> Products { get; set;}
        public DbSet<Tenant> Tenants { get; set; }

        // public override int SaveChanges()
        // {      
        //     _logger.LogInformation($"Tenant from header context: {CurrentTenantId}");
        //     // _logger.LogInformation($"Tenant from header: {tenant}");  
        //     foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList()) 
        //     {
        //         switch (entry.State)
        //         {
        //             case EntityState.Added:
        //             case EntityState.Modified:
        //                 entry.Entity.TenantId = CurrentTenantId; 
        //                 break;
        //         }
        //     }
        //     var result = base.SaveChanges();
        //     return result;
        // }

        // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        // {
        //     foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
        //     {
        //         switch (entry.State)
        //         {
        //             case EntityState.Added:
        //             case EntityState.Modified:
        //                 entry.Entity.TenantId = CurrentTenantId;
        //                 break;
        //         }
        //     }
        //     var result = await base.SaveChangesAsync(cancellationToken);
        //     return result;
        // }

        // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        // {
        //     foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
        //     {
        //         switch (entry.State)
        //         {
        //             case EntityState.Added:
        //             case EntityState.Modified:
        //                 entry.Entity.TenantId = CurrentTenantId;
        //                 break;
        //         }
        //     }
        //     var result = await base.SaveChangesAsync(cancellationToken);
        //     return result;
        // }

        // public override int SaveChanges()
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList()) 
            {
                _logger.LogInformation($"Tenant from header context: {CurrentTenantId}");
                _logger.LogInformation($"Entity state: {entry.State}");
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.TenantId = CurrentTenantId; 
                        break;
                    case EntityState.Modified:
                        // Add logic here if you want to handle modified entities differently
                        break;
                }
            }
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes");
                throw; // Re-throw the exception to propagate it further
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {         
            builder.Entity<Product>().HasQueryFilter(a => a.TenantId == CurrentTenantId); 
        }

    }
}