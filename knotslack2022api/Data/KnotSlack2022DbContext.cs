using knotslack2022api.Models;
using Microsoft.EntityFrameworkCore;

namespace Knotslack2022api.Data
{
    public class KnotSlack2022DbContext : DbContext
    {
        public KnotSlack2022DbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Message> Messages { get; set; }
    }
}
