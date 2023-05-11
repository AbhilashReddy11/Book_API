using System.ComponentModel.DataAnnotations;

namespace Book_API.Models.DTO
{
    public class AuthorUpdateDTO
    {
        [Required]
        public int AID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
