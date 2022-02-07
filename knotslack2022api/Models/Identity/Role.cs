using System.ComponentModel.DataAnnotations;

namespace knotslack2022api.Models.Identity
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
