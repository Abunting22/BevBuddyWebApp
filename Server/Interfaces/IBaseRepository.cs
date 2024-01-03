using System.Data;

namespace BevBuddyWebApp.Server.Interfaces
{
    public interface IBaseRepository
    {
        public IDbConnection Connect();
    }
}
