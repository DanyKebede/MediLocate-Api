namespace MediLast.Abstractions.Interfaces
{
    public interface IPresenceRepository
    {
        Task UserConnected(string username, string connectionId);
        Task UserDisconnected(string username, string connectionId);
        Task<string[]> GetOnlineUsers();
    }
}
