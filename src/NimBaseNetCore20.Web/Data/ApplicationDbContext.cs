using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NimBaseNetCore20.Data
{
    public class ApplicationDbContext : IdentityDbContext<NimBaseNetCore20.Domain.ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<NimBaseNetCore20.Domain.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<NimBaseNetCore20.Domain.Tenant> Tenants { get; set; }

        public DbSet<NimBaseNetCore20.Domain.TenantType> TenantTypes { get; set; }
    }
}
