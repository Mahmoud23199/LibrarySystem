using LibrarySystem.Models;
using LibrarySystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositry
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _context;

        public UserService(LibraryContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    

        public async Task<User> GetUserAsync(string userName)
        {
             return await _context.Users
                .Include(u => u.BorrowedBooks)
                .FirstOrDefaultAsync(u => u.Name == userName);
        }

        public async Task<BorrowedViewModel> GetUserWithBorrowedBooksAsync(string userName)
        {
            var user =await GetUserAsync(userName);
            if(user == null)
            {
                throw new Exception($"User with name:{userName} not borrow any books");
            }
            else
            {
                var result = await _context.Users.Where(u => u.Id == user.Id).Select(i=> new BorrowedViewModel
                {
                    User=i.Name,
                    UserId=i.Id,
                    BorrowBooks=i.BorrowedBooks.Select(b=>new BorrowedBookNameViewModel
                    {
                        BorrowedDate=b.BorrowedDate,
                        BookCopyCode=b.BookCopy.Code,
                        bookname=b.BookCopy.Book.Name,
                        ReturnedDate=b.ReturnedDate,
                        bookCopyId=b.BookCopy.Id

                    }).ToList()
                }).FirstOrDefaultAsync();
                return result;

            }
        }
    }
}
