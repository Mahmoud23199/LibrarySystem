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
        public async Task<IActionResult> Update(int Id)
        {
            return View(await _libraryService.GetBookAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookViewModel bookView)
        {
            try
            {
                var book = new Book
                {
                    Author = bookView.AuthorName, Name = bookView.Name, Id = bookView.Id
                };
                await _libraryService.UpdateBookAsync(book,bookView.NumberOfCopies);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
       
        public async Task<IActionResult> Delete(int Id)
        {
            return View(await _libraryService.GetBookAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int Id)
        {
            try
            {
                await _libraryService.DeleteBookAsync(Id);
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

