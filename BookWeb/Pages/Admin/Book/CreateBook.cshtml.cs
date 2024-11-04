using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryRepositories;
using BusinessLayer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BookWeb.Pages.Admin
{
    public class CreateBookModel : PageModel
    {
        private readonly IHubContext<SignalRServer> _hubContext;
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateBookModel(IBookRepository bookRepository, ICategoryRepository categoryRepository, IHubContext<SignalRServer> hubContext)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Books Book { get; set; }

        public List<Categories> CategoriesList { get; set; }

        public async Task OnGetAsync()
        {
            CategoriesList = (await _categoryRepository.GetCategoryAll()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CategoriesList = (await _categoryRepository.GetCategoryAll()).ToList();
            await _bookRepository.Add(Book);
            await _hubContext.Clients.All.SendAsync("ReceiveUpdate", $"Book with Name {Book.BookName} has been Created.");
            return RedirectToPage("Books");
        }
    }
}
