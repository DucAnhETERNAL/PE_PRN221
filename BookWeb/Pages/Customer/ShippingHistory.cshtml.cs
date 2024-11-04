using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryRepositories;
using BusinessLayer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace BookWeb.Pages
{
    [Authorize]
   
    public class ShippingHistoryModel : PageModel
    {
        private readonly IShipRepository _shipRepository;
        private readonly IHubContext<SignalRServer> _hubContext;
        public ShippingHistoryModel(IShipRepository shipRepository, IHubContext<SignalRServer> hubContext)
        {
            _shipRepository = shipRepository;
            _hubContext = hubContext;
        }

        public List<ShippingViewModel> ShippingList { get; set; }

        public async Task OnGetAsync()
        {
            int userId = GetCurrentUserId();
            var ships = await _shipRepository.GetShipAllByUserId(userId);

            ShippingList = ships.Select(s => new ShippingViewModel
            {
                ShipID = s.ShipID,
                BookName = s.Books != null ? s.Books.BookName : "Unknown",
                DateOrder = s.DateOrder,
                DateShip = s.DateShip,
                UserApproveName = s.UsersApprove != null ? s.UsersApprove.UserName : "Unknown",
                Status = s.IsApproved ? "Approved" : "Not yet approved"
            }).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _shipRepository.Delete(id);
            
            return RedirectToPage();
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }
    }

    public class ShippingViewModel
    {
        public int ShipID { get; set; }
        public string BookName { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime? DateShip { get; set; }
        public string UserApproveName { get; set; }
        public string Status { get; set; }
    }
}
