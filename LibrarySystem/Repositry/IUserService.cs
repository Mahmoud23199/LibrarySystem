using LibrarySystem.Models;

namespace LibrarySystem.Repositry
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string userName);
        Task AddUserAsync(User user);
    }
}
