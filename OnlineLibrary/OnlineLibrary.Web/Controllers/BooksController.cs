using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Service.Interface;

namespace OnlineLibrary.Web.Controllers
{
    public class BooksController(IBookService bookService, IAuthorService authorService, IOrderService orderService) : Controller
    {
        private readonly IBookService bookService = bookService;
        private readonly IAuthorService authorService = authorService;

        // GET: Books
        public async Task<IActionResult> Index(string title, Guid? authorId, bool? inStock)
        {
            ViewData["AuthorId"] = new SelectList(authorService.GetAllAuthours(), "Id", "Name");

            return View(bookService.GetAllBooksFiltered(title, authorId, inStock).ToList());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetBookDetailsById(id);

            if (book == null)
            {
                return NotFound();
            }

            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(authorService.GetAllAuthours(), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,AuthorId,Price,BookCoverUrl,Pages,QuantityInInventory,Id")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                bookService.CreateNewBook(book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(authorService.GetAllAuthours(), "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetBookDetailsById(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(authorService.GetAllAuthours(), "Id", "Name", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,AuthorId,Price,BookCoverUrl,Pages,QuantityInInventory,Id")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bookService.EditBook(book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(authorService.GetAllAuthours(), "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetBookDetailsById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RentedBooksHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var books = bookService.GetRentedBooksForUser(userId);

            return View(books);
        }

        [HttpPost]
        public ActionResult ReturnBook(Guid rentedId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bookService.ReturnBook(userId, rentedId);

            return RedirectToAction("RentedBooksHistory", "Books");
        }
    }
}
