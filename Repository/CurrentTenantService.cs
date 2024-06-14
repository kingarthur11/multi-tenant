using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using Serilog;

namespace MyProject.Repository
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _context;
        public string? TenantId { get; set; }
        private readonly ILogger<CurrentTenantService> _logger;

        public CurrentTenantService(ILogger<CurrentTenantService> logger, TenantDbContext context)
        {
            _context = context;
            _logger = logger;

        }
        public async Task<bool> SetTenant(string tenant)
        {

            var tenantInfo = await _context.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync();
            if (tenantInfo != null)
            {
                TenantId = tenant;
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid"); 
            }

        }
    }
}