using BevBuddyWebApp.Shared.Models;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IJwtServices
    {
        public string GetJwt(User user);
    }
}
