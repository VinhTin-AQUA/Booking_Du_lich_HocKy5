using Booking.Models;

namespace Booking.Interfaces
{
    public interface IBookTourDetailRepository
    {
        public Task<bool> Save();
        public Task<bool> AddBookTourDetail(BookTourDetail detail);
        public Task<ICollection<BookTourDetail>> GetBookTourDetailByBookTourId(int bookTourId);
        public Task<bool> DeleteBookTour(BookTourDetail detail);
        public Task<bool> UpdateBookTour(BookTourDetail detail);
    }
}
