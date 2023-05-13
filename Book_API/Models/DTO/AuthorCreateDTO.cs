using System.ComponentModel.DataAnnotations;

namespace Book_API.Models.DTO
{
    public class AuthorCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
