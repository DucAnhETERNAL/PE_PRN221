using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryRepositories;
using BusinessLayer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BookWeb.Pages.Admin
{
    public class EditBookModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHubContext<SignalRServer> _hubContext;
        public EditBookModel(IBookRepository bookRepository, ICategoryRepository categoryRepository, IHubContext<SignalRServer> hubContext)
        {
            _hubContext = hubContext;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
        public Books Book { get; set; }
        public List<Categories> CategoriesList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _bookRepository.GetBookById(id);

            if (Book == null)
            {
                return NotFound();
            }

            CategoriesList = (await _categoryRepository.GetCategoryAll()).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _bookRepository.Update(Book);
            await _hubContext.Clients.All.SendAsync("ReceiveUpdate", $"Book {Book.BookName} has been Updated.");
            return RedirectToPage("Books");
        }
    }
}
