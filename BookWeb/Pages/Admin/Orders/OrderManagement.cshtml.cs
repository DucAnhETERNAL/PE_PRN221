using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryRepositories;
using BusinessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.SignalR;

namespace BookWeb.Pages.Admin
{
    public class OrderManagementModel : PageModel
    {
        private readonly IShipRepository _shipRepository;
        private readonly IHubContext<SignalRServer> _hubContext;
        public OrderManagementModel(IShipRepository shipRepository, IHubContext<SignalRServer> hubContext)
        {
            _hubContext = hubContext;
            _shipRepository = shipRepository;
        }

        public List<OrderViewModel> OrdersList { get; set; }

        public async Task OnGetAsync()
        {
            var orders = await _shipRepository.GetShipAll();
            OrdersList = orders.Select(o => new OrderViewModel
            {
                ShipID = o.ShipID,
                BookName = o.Books != null ? o.Books.BookName : "Unknown",
                DateOrder = o.DateOrder,
                DateShip = o.DateShip,
                UserOrderName = o.UsersOrder != null ? o.UsersOrder.UserName : "Unknown",
                UserApproveName = o.UsersApprove != null ? o.UsersApprove.UserName : "Not Approved",
                Status = o.IsApproved ? "Approved" : "Pending"
            }).ToList();
        }

        public async Task<IActionResult> OnPostConfirmAsync(int id)
        {
            var ship = await _shipRepository.GetShipAllById(id);
            if (ship != null)
            {
                ship.IsApproved = true;
                ship.DateShip = DateTime.Now;
                await _shipRepository.Update(ship);
                await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "Order confirmed.");
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _shipRepository.Delete(id);
            await _hubContext.Clients.All.SendAsync("ReceiveUpdate", "Order deleted.");
            return RedirectToPage();
        }

        public class OrderViewModel
        {
            public int ShipID { get; set; }
            public string BookName { get; set; }
            public DateTime DateOrder { get; set; }
            public DateTime? DateShip { get; set; }
            public string UserOrderName { get; set; }
            public string UserApproveName { get; set; }
            public string Status { get; set; }
        }
    }
}
