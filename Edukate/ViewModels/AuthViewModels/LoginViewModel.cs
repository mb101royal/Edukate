using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.AuthViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "This field is required."), MaxLength(64)]
        public string UsernameOrEmail { get; set; }

        [
            Required(ErrorMessage = "Password is required."),
            DataType(DataType.Password),
            Display(Name = "Password"),
            MinLength(6),
            RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$")
        ]
        public string Password { get; set; }
    }
}
