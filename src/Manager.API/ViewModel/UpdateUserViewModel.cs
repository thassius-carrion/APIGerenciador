using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModel
{
    public class UpdateUserViewModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
