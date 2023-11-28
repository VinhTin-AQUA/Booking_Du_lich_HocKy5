using Booking.Models;

namespace Booking.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<bool> Save();
        public Task<Category> GetCategoryById(int? id);
        public Task<bool> AddCategory(Category category);
        public Task<bool> CategoryExisted(string name);
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<bool> Delete(Category category);
        public Task<bool> UpdateCategory(Category category);
    }
}
