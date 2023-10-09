using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IUserManagerRepository
    {
        public Task<IEnumerable<ApplicationUser>> GetUser(int currentPage, int pageSize);

        public int TotalUsers();
    }
}
