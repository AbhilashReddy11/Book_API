using System.ComponentModel.DataAnnotations;

namespace Book_API.Models.DTO
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
