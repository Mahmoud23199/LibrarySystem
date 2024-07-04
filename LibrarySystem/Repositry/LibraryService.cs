using LibrarySystem.Models;
using LibrarySystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositry
{
    public class LibraryService:ILibraryService
    {
        private readonly LibraryContext _context;

        public LibraryService(LibraryContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book, int numberOfCopies)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            for (int i = 0; i < numberOfCopies; i++)
            {
                _context.BookCopies.Add(new BookCopy
                {
                    BookId = book.Id,
                    IsBorrowed = false,
                    Code = Guid.NewGuid().ToString()
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) throw new Exception("Book not found.");

            var bookCopies = await _context.BookCopies.Where(bc => bc.BookId == bookId).ToListAsync();
            _context.BookCopies.RemoveRange(bookCopies);

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        //public async Task BorrowBookAsync(int userId, int bookId)
        //{
        //    var alreadyBorrowed = await _context.BorrowedBooks
        //        .AnyAsync(bb => bb.UserId == userId && bb.BookCopy.BookId == bookId && bb.ReturnedDate == null);

        //    if (alreadyBorrowed)
        //        throw new Exception("You have already borrowed a copy of this book and have not returned it yet.");

        //    var bookCopy = await _context.BookCopies
        //        .Where(bc => bc.BookId == bookId && !bc.IsBorrowed)
        //        .FirstOrDefaultAsync();

        //    if (bookCopy == null)
        //        throw new Exception("No available copies.");

        //    bookCopy.IsBorrowed = true;
        //    _context.BorrowedBooks.Add(new BorrowedBook
        //    {
        //        UserId = userId,
        //        BookCopyId = bookCopy.Id,
        //        BorrowedDate = DateTime.Now
        //    });

        //    await _context.SaveChangesAsync();
        //}

        public async Task BorrowBookAsync(int userId, int bookId)
        {
            
            var alreadyBorrowed = await _context.BorrowedBooks
                .AnyAsync(bb => bb.UserId == userId && bb.BookCopy.BookId == bookId && bb.ReturnedDate == null);

            if (alreadyBorrowed)
                throw new Exception("You have already borrowed a copy of this book and have not returned it yet.");

            var bookCopy = await _context.BookCopies
                .Where(bc => bc.BookId == bookId && !bc.IsBorrowed)
                .FirstOrDefaultAsync();

            if (bookCopy == null)
                throw new Exception("No available copies.");

            bookCopy.IsBorrowed = true;

            _context.BorrowedBooks.Add(new BorrowedBook
            {
                UserId = userId,
                BookCopyId = bookCopy.Id,
                BorrowedDate = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }
        public async Task ReturnBookAsync(int userId, int bookCopyId)
        {
            var borrowedBook = await _context.BorrowedBooks
                .Where(bb => bb.UserId == userId && bb.BookCopyId == bookCopyId && bb.ReturnedDate == null)
                .FirstOrDefaultAsync();

            if (borrowedBook == null)
                throw new Exception("This book was not borrowed by this user.");

            borrowedBook.ReturnedDate = DateTime.Now;
            var bookCopy = await _context.BookCopies.FindAsync(bookCopyId);
            bookCopy.IsBorrowed = false;

            await _context.SaveChangesAsync();
        }

        public async Task<List<BookViewModel>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Copies)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    AuthorName = b.Author,
                    NumberOfCopies = b.Copies.Count,
                    BookCopies = b.Copies.Select(bc => new BookCopyViewModel
                    {
                        Id = bc.Id,
                        Code = bc.Code,
                        IsBorrowed = bc.IsBorrowed
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<BookViewModel> GetBookAsync(int id)
        {
            return await _context.Books.Include(b => b.Copies).Where(i => i.Id == id)
                 .Select(b => new BookViewModel
                 {
                     Id = b.Id,
                     Name = b.Name,
                     AuthorName = b.Author,
                     NumberOfCopies = b.Copies.Count,
                     BookCopies = b.Copies.Select(bc => new BookCopyViewModel
                     {
                         Id = bc.Id,
                         Code = bc.Code,
                         IsBorrowed = bc.IsBorrowed
                     }).ToList()
                 }).FirstOrDefaultAsync();
           
        }

    }
}

    

