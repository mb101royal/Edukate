using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Edukate.Models
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(64)]
        public string Fullname { get; set; }
    }
}
