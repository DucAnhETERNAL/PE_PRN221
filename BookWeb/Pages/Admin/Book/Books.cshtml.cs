using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryRepositories;
using BusinessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BookWeb.Pages.Admin
{
    public class BooksModel : PageModel
    {
        private readonly IHubContext<SignalRServer> _hubContext;
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BooksModel(IBookRepository bookRepository, ICategoryRepository categoryRepository, IHubContext<SignalRServer> hubContext)
        {
            _hubContext = hubContext;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public List<BookViewModel> BooksList { get; set; }
        public List<Categories> CategoriesList { get; set; }

        public async Task OnGetAsync()
        {
            var books = await _bookRepository.GetBookAll();
            BooksList = books.Select(b => new BookViewModel
            {
                BookID = b.BookID,
                BookName = b.BookName,
                Price = b.Price,
                CategoryID = b.Category != null ? b.Category.CategoryID : 0,
                CategoryName = b.Category != null ? b.Category.CategoryName : "Unknown"
            }).ToList();

            CategoriesList = (await _categoryRepository.GetCategoryAll()).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _bookRepository.Delete(id);
            await _hubContext.Clients.Group("Admins").SendAsync("ReceiveUpdate", $"Book with ID {id} has been deleted.");

            return RedirectToPage();
        }
    }

    public class BookViewModel
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
