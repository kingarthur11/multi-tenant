using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyProject.Repository;

namespace MyProject.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TenantResolver> _logger;
        public TenantResolver(ILogger<TenantResolver> logger, RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader);
            
           
            if (string.IsNullOrEmpty(tenantFromHeader) == false)
            {
                await currentTenantService.SetTenant(tenantFromHeader);
                
            }
            await _next(context);
        }
    }
}