using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryRepositories;
using BusinessLayer;

namespace BookWeb.Pages
{
    public class BookListModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public BookListModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Books> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _bookRepository.GetBookAll();
        }
    }
}
