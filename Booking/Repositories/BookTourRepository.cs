using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.EntityFrameworkCore;


namespace Booking.Repositories
{
    public class BookTourRepository : IBookTourRepository
    {
        private readonly BookingContext context;

        public BookTourRepository(BookingContext context) { this.context = context; }
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

        public async Task<BookTour?> GetBookTourByID(int bookTourId)
        {
            var bookTour = await context.BookTours
                .Where(bt => bt.BookTourId == bookTourId)
                .FirstOrDefaultAsync();
            return bookTour;
        }

        public Task<BookTour?> GetNewBookTour()
        {
            var newbt = context.BookTours.OrderByDescending(bt => bt.BookTourId).FirstOrDefaultAsync();
            return newbt;
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
