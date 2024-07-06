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

            return View(book);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string UserName)
        {
           var IsExist=await _userService.GetUserByIdAsync(UserName);
            if (IsExist ==null)
            {
                var user = new User
                {
                    Name = UserName,
                };
                _userService.AddUserAsync(user);

                //some logic
                return View();
            }
            return View();

        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
