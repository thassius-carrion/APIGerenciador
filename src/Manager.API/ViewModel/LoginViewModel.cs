using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login can not be empty.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password can not be empty.")]
        public string Password { get; set; }
    }
}
