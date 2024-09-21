using mediAPI.Extensions;
using MediLast.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MediLast.Hubs
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly IPresenceRepository _presenceRepository;

        public PresenceHub(IPresenceRepository presenceRepository)
        {
            _presenceRepository = presenceRepository;
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Prescence: " + Context.User.GetUserName());
            await _presenceRepository.UserConnected(Context.User.GetUserName(), Context.ConnectionId);

            await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUserName());

            var currentUsers = await _presenceRepository.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _presenceRepository.UserDisconnected(Context.User.GetUserName(), Context.ConnectionId);

            await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUserName());

            var currentUsers = await _presenceRepository.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);

            await base.OnDisconnectedAsync(exception);
        }
    }
}