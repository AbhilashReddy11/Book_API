using System.ComponentModel.DataAnnotations;

namespace Book_API.Models.DTO
{
    public class BookCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int AuthorID { get; set; }
        [Required]
        public int PublisherID { get; set; }
        public string ISBN { get; set; }
        public int publicationyear { get; set; }
    }
}
