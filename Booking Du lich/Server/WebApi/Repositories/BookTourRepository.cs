using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class BookTourRepository : IBookTourRepository
    {
        private readonly ApplicationDbContext context;

        public BookTourRepository(ApplicationDbContext context) { this.context = context; }
        public async Task<bool> AddBookTour(BookTour bookTour)
        {
            context.BookTours.Add(bookTour);
            return await Save();
        }

        public async Task<bool> DeleteBookTour(BookTour bookTour)
        {
            if (bookTour == null)
            {
                return false;
            }
            context.BookTours.Remove(bookTour);
            return await Save();
        }

        public async Task<ICollection<BookTour>> GetAllBookTour()
        {
            var bookTours = await context.BookTours.ToListAsync();
            return bookTours;
        }

        public async Task<BookTour> GetBookTourByID(string userID, int packageID, DateTime? DepartureDate)
        {
            var bookTour = await context.BookTours
                .Where(bt => bt.UserID == userID && bt.DepartureDate == DepartureDate && bt.PackageId == packageID)
                .FirstOrDefaultAsync();
            return bookTour;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> UpdateBookTour(BookTour bookTour)
        {
            context.BookTours.Update(bookTour);
            return await Save();
        }
    }
}
