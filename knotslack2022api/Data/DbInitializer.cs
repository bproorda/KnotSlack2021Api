using knotslack2022api.Models.Identity;
using knotslack2022api.Models;
using Knotslack2022api.Data;
using knotslack2022api.Services;

namespace knotslack2022api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(KnotSlack2022DbContext context, IUserManager userManger)
        {
            if(userManger != null && !context.Roles.Any())
            {
                Console.WriteLine("Seeding Roles... ");
                userManger.InitializeRolesAsync();
            }
            if (!context.Channels.Any())
            {
                Console.WriteLine("Seeding Channels... ");
               
                context.Channels.AddRange(
                    new Channel() { Name = "General", Type = "General" }
                );
                context.SaveChanges();

                return;
            }

        }
    }
}
