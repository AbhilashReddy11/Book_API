using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_API.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public Author author { get; set; }
        [ForeignKey("Publisher")]
        public int PublisherID { get; set; }
        public Publisher publisher { get; set; }
         public string ISBN { get; set; }
        public int publicationyear { get; set; }



    }
}
