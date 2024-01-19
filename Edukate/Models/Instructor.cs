using System.ComponentModel.DataAnnotations;

namespace Edukate.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required, MaxLength(64)]
        public string Profession { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
