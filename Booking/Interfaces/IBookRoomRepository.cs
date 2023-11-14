using Booking.Models;

namespace Booking.Interfaces
{
    public interface IBookRoomRepository
    {
        public Task<bool> Save();
        public Task<bool> AddBookRoom(BookRoom bookRoom);
        public Task<ICollection<BookRoom>> GetAllBookRooms();
        public Task<BookRoom?> GetBookRoomByID(string userID, int roomID, DateTime? checkInDate);

        public Task<bool> DeleteBookRoom(BookRoom bookRoom);

        public Task<bool> UpdateBookRoom(BookRoom bookRoom);
    }
}
