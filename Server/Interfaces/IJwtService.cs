using BevBuddyWebApp.Shared.Models;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IJwtService
    {
        public string GetJwt(User user);
    }
}
