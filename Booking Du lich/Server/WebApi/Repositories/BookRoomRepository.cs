using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class BookRoomRepository : IBookRoomRepository
    {
        private readonly ApplicationDbContext context;

        public BookRoomRepository(ApplicationDbContext context) { this.context = context; }
        public async Task<bool> AddBookRoom(BookRoom bookRoom)
        {
            context.BookRooms.Add(bookRoom);
            return await Save();
        }

        public async Task<bool> DeleteBookRoom(BookRoom bookRoom)
        {
            if (bookRoom == null)
            {
                return false;
            }
            context.BookRooms.Remove(bookRoom);
            return await Save();
        }

        public async Task<ICollection<BookRoom>> GetAllBookRooms()
        {
            var bookRooms = await context.BookRooms.ToListAsync();
            return bookRooms;
        }

        public async Task<BookRoom> GetBookRoomByID(string userID, int roomID, DateTime? checkInDate)
        {
            var bookRoom = await context.BookRooms
                .Where(br => br.UserID == userID && br.CheckInDate == checkInDate && br.RoomID == roomID)
                .FirstOrDefaultAsync();
            return bookRoom;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }



        public async Task<bool> UpdateBookRoom(BookRoom bookRoom)
        {
            context.BookRooms.Update(bookRoom);
            return await Save();
        }
    }
}
