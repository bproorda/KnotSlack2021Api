namespace knotslack2022api.Models.Identity.DTO
{
    public class UserWithToken
    {
        public string UserId { get; set; }
        public string Token { get; set; }

        public DateTime LastVisited { get; set; }
        //public List<createChannelDTO> Channels { get; set; }

        public List<string> Roles { get; set; }
    }
}
