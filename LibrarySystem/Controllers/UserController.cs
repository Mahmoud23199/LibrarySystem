using LibrarySystem.Models;
using LibrarySystem.Repositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILibraryService _libraryService;


        public UserController(IUserService userService, ILibraryService libraryService)
        {
            _userService = userService;
            _libraryService = libraryService;
        }
        // GET: UserController
        public async Task<ActionResult> Index(int Id)
        {
            var book =await _libraryService.GetBookAsync(Id);
            if (book == null)
            {
                return NotFound();
            }

            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }

            return View(book);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string UserName,int bookId)
        {
           var IsExist=await _userService.GetUserAsync(UserName);

            if (IsExist ==null)
            {
                var user = new User
                {
                    Name = UserName,
                };
               await _userService.AddUserAsync(user);

                //some logic
                return RedirectToAction("BorrowBook", new { userId =user.Id, bookId=bookId });
            }
            return RedirectToAction("BorrowBook", new { userId = IsExist.Id, bookId = bookId });


        }
        [HttpGet]
        public async Task<IActionResult> BorrowBook(int userId, int bookId)
        {
            try
            {
                await _libraryService.BorrowBookAsync(userId, bookId);
                return RedirectToAction("Index","Library");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", new { Id = bookId });
            }
        }
        public ActionResult borrowerUser()
        {

            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"].ToString();
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> borrowerUserList(string UserName)
        {

            try
            {
                var viewModel = await _userService.GetUserWithBorrowedBooksAsync(UserName);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("borrowerUser"); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReturnBook(int userId, int bookCopyId,string UserName)
        {
            try
            {
                await _libraryService.ReturnBookAsync(userId, bookCopyId);

                return RedirectToAction("borrowerUserList",new { UserName });
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

    }
}
