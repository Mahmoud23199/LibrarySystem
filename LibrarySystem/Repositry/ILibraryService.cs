using LibrarySystem.Models;
using LibrarySystem.ViewModel;

namespace LibrarySystem.Repositry
{
    public interface ILibraryService
    {
        Task AddBookAsync(Book book, int numberOfCopies);
        Task<List<BookViewModel>> GetAllBooksAsync();
        Task<BookViewModel> GetBookAsync(int id);
        Task UpdateBookAsync(Book book, int numberOfCopies);
        Task DeleteBookAsync(int bookId);
        Task BorrowBookAsync(int userId, int bookId);
        Task ReturnBookAsync(int userId, int bookCopyId);
    }
}
