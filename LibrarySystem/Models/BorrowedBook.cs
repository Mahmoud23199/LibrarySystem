using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Models
{
    public class BorrowedBook
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }


        [ForeignKey("BookCopy")]
        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }

        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
