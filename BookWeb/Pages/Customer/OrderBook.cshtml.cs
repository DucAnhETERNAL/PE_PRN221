using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using LibraryRepositories;
using BusinessLayer;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BookWeb.Pages
{
    [Authorize]
    public class OrderBookModel : PageModel
    {
        private readonly IHubContext<SignalRServer> _hubContext;
        private readonly IBookRepository _bookRepository;
        private readonly IShipRepository _shipRepository;
        public OrderBookModel(IBookRepository bookRepository, IShipRepository shipRepository, IHubContext<SignalRServer> hubContext)
        {
            _bookRepository = bookRepository;
            _shipRepository = shipRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public int BookID { get; set; }

        public Books Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _bookRepository.GetBookById(id);

            if (Book == null)
            {
                return NotFound();
            }

            BookID = id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var book = await _bookRepository.GetBookById(BookID);
            if (book == null)
            {
                return NotFound();
            }


            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var newShip = new Ships
            {
                DateOrder = DateTime.Now,
                IsApproved = false,
                DateShip = null,
                BookID = book.BookID,
                UserOrderID = userId,
                UserApproveID = 1
            };


            await _shipRepository.Add(newShip);
           

            TempData["SuccessMessage"] = $"You have bought '{book.BookName}' successfully!";


            return RedirectToPage("ShippingHistory");
        }

    }
}
