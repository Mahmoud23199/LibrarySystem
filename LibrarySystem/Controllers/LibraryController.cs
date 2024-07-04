using LibrarySystem.Models;
using LibrarySystem.Repositry;
using LibrarySystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }
        public async Task<IActionResult> Index()
        {
            List<BookViewModel> books = await _libraryService.GetAllBooksAsync();
            return View(books);
        }

        public async Task<IActionResult> Create()
        {

           return View();
        }

        [HttpPost]  
        public async Task<IActionResult> AddBook(CreateBookViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var book = new Book { Author = model.AuthorName, Name = model.Name };

                    await _libraryService.AddBookAsync(book, model.NumberOfCopies);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            try
            {
                await _libraryService.UpdateBookAsync(book);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            try
            {
                await _libraryService.DeleteBookAsync(bookId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(int userId, int bookId)
        {
            try
            {
                await _libraryService.BorrowBookAsync(userId, bookId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(int userId, int bookCopyId)
        {
            try
            {
                await _libraryService.ReturnBookAsync(userId, bookCopyId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}

