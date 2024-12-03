using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventManagementWeb.Models;

namespace EventManagementWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<EventManagementUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventManagementWeb.Models.Event> Events { get; set; }
        public DbSet<EventManagementWeb.Models.Location> Locations { get; set; }
        public DbSet<EventManagementWeb.Models.EventManagementUser> EventManagementUsers { get; set; }
        public DbSet<EventManagementWeb.Models.EventStaff> EventStaff { get; set; } = default!;
        public DbSet<EventManagementWeb.Models.Language> Languages { get; set; } = default!;
    }
}
