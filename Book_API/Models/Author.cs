using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Book_API.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AID { get; set; }
        [Required]
        public string Name { get; set; }

      //  [Required]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
       
        
    }
}
