using LibrarySystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.ViewModel
{
    public class BorrowedViewModel
    {
       
        public string User { get; set; }
        public int UserId { get; set; }

        public List<BorrowedBookNameViewModel> BorrowBooks { get; set; }
        
    }
    public class BorrowedBookNameViewModel
    {
        public string bookname { get; set; }
        public int bookCopyId { get; set; }
        public string BookCopyCode { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime ?ReturnedDate { get; set; }


    }


}
