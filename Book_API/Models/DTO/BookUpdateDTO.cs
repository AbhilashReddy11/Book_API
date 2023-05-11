using System.ComponentModel.DataAnnotations;

namespace Book_API.Models.DTO
{
    public class BookUpdateDTO
    {
        [Required]
        public int ID { get; set; }
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
