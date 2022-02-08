using System.ComponentModel.DataAnnotations;

namespace knotslack2022api.Models
{
    public class Channel
    {
        [Key]
        [Required]
        public string Name { get; set; }

        public string Type { get; set; }

        //public List<UserChannel> UserChannels { get; set; }
    }
}
