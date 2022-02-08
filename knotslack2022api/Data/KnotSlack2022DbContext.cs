using knotslack2022api.Models;
using knotslack2022api.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Knotslack2022api.Data
{
    public class KnotSlack2022DbContext : IdentityDbContext<KSUser>
    {
        public KnotSlack2022DbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<KSUser> KSUsers { get; set; }
        public DbSet<Channel> Channels { get; set; }
    }
}
