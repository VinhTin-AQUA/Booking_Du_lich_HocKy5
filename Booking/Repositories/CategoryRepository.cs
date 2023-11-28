using Microsoft.EntityFrameworkCore;
using Booking.Interfaces;
using Booking.Models;
using Booking.Data;

namespace WebApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookingContext context;
        public CategoryRepository(BookingContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddCategory(Category category)
        {
            context.Categories.Add(category);
            return await Save();
        }

        public async Task<bool> Delete(Category category)
        {
            if (category == null)
            {
                return false;
            }
            context.Categories.Remove(category);
            return await Save();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = await context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            var category = await context.Categories.Where(c => c.CategoryId == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task<bool> Save()
        {
            var s = await context.SaveChangesAsync();
            return s > 0;
        }

        public async Task<bool> CategoryExisted(string name)
        {
            var type = await context.Categories.Where(c => c.CategoryName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            return type != null;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            return await Save();
        }
    }
}
