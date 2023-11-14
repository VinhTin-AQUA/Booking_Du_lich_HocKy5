using Booking.Models;

namespace Booking.Interfaces
{
    public interface IBookTourRepository
    {
        public Task<bool> Save();
        public Task<bool> AddBookTour(BookTour bookTour);
        public Task<ICollection<BookTour>> GetAllBookTour();
        public Task<BookTour?> GetBookTourByID(string userID, int packageID, DateTime? DepartureDate);

        public Task<bool> DeleteBookTour(BookTour bookTour);

        public Task<bool> UpdateBookTour(BookTour bookTour);
    }
}
