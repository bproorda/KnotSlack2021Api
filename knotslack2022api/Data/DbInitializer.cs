using knotslack2022api.Models.Identity;
using knotslack2022api.Models;
using Knotslack2022api.Data;

namespace knotslack2022api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(KnotSlack2022DbContext context)
        {
            if (!context.Channels.Any())
            {
                System.Console.WriteLine("Seeding Data... ");
               
                context.Channels.AddRange(
                    new Channel() { Name = "General", Type = "General" }
                );
                context.SaveChanges();

                return;
            }

            System.Console.WriteLine("Data Already Seeded");

        }
    }
}
