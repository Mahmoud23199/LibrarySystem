using Microsoft.EntityFrameworkCore;
using LibrarySystem.ViewModel;

namespace LibrarySystem.Models
{
    public class LibraryContext:DbContext
    {
       
        public LibraryContext(DbContextOptions<LibraryContext> options): base(options)
        {
                
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<LibrarySystem.ViewModel.BookViewModel> BookViewModel { get; set; } = default!;
    }
}
