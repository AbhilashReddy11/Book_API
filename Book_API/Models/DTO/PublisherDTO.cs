using System.ComponentModel.DataAnnotations;

namespace Book_API.Models.DTO
{
    public class PublisherDTO
    {
        [Required]
        public int PID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
