using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entity
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string Name { get; set; } 
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
       
    }
}
