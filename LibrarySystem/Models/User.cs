using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
