using LibrarySystem.Models;
using LibrarySystem.ViewModel;

namespace LibrarySystem.Repositry
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string userName);
        Task AddUserAsync(User user);
        Task<BorrowedViewModel> GetUserWithBorrowedBooksAsync(string userName);
    }
}
