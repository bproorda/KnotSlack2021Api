using Microsoft.AspNetCore.Identity;

namespace knotslack2022api.Models.Identity
{
    public class KSUser : IdentityUser
    {
        public bool LoggedIn { get; set; }

        public DateTime LastVisited { get; set; }

        public string? ConnectionId { get; set; }

        // Nav Prop for Many to Many/One to Many Relationships
        //public List<UserChannel> UserChannels { get; set; }

        //public List<UserMessage> UserMessages { get; set; }
    }
}
