using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Repositories
{
    public class BookTourDetailRepository : IBookTourDetailRepository
    {
        private readonly BookingContext context;

        public BookTourDetailRepository(BookingContext context) { 
            this.context = context; 
        }
        public async Task<bool> AddBookTourDetail(BookTourDetail detail)
        {
            context.BookTourDetails.Add(detail);
            return await Save();
        }

        public async Task<bool> DeleteBookTour(BookTourDetail detail)
        {
            if(detail == null)
            {
                return false;
            }
            context.BookTourDetails.Remove(detail);
            return await Save();
        }

        public async Task<ICollection<BookTourDetail>> GetBookTourDetailByBookTourId(int bookTourId)
        {
            var btDetail = await context.BookTourDetails.Where(btd => btd.BookTourId == bookTourId).ToListAsync();
            return btDetail;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> UpdateBookTour(BookTourDetail detail)
        {
            context.BookTourDetails.Update(detail);
            return await Save();
        }
    }
}
