using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.InstructorsViewModel
{
    public class InstructorCreateViewModel
    {
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [Required, MaxLength(64)]
        public string Profession { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
