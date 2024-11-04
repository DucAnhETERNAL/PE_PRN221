using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BookWeb
{
    public class SignalRServer :Hub
    {
        public override async Task OnConnectedAsync()
        {
            // Check if the user is an admin
            var isAdmin = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Admin";

            if (isAdmin)
            {
                // Add the connection to the "Admins" group
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Remove from "Admins" group when disconnected
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
