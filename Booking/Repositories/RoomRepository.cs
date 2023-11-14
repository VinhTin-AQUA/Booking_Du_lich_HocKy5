using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingContext _context;

        public RoomRepository(BookingContext context) {
            _context = context;
        }
        public async Task<bool> AddRoom(Room room)
        {
            _context.Room.Add(room);
            return await Save();
        }

        public async Task<bool> DeleteRoom(Room room)
        {
            if (room == null)
            {
                return false;
            }
            _context.Room.Remove(room);
            return await Save();
        }

        public async Task<ICollection<Room>> GetAllRooms()
        {
            var rooms =await _context.Room.ToListAsync();
            return rooms;
        }

        public async Task<ICollection<Room>> GetAllRoomsOfHotel(int hotelId, string searchString = "")
        {
            if (string.IsNullOrEmpty(searchString)) 
            {
                var _rooms = await _context.Room
                    .Where(r => r.HotelId == hotelId)
                    .Include(r => r.Hotel)
                    .Include(r => r.RoomPrices)
                    .Include(r => r.RoomType)
                    .ToListAsync();
                return _rooms;
            }

            var rooms = await _context.Room
                .Where(r => r.HotelId == hotelId && r.RoomName.ToLower().Contains(searchString.ToLower()))
                .Include(r => r.Hotel)
                .Include(r => r.RoomPrices)
                .Include(r => r.RoomType)
                .ToListAsync();
            return rooms;
        }

        public async Task<IEnumerable<Room>> SearchRoomOfHotel(int? HotelId)
        {
            var rooms = await _context.Room
                .Where(r => r.HotelId == HotelId)
                .Include(r => r.Hotel)
                .Include(r => r.RoomPrices)
                .Include(r => r.RoomType)
                .ToListAsync();
               
            return rooms;
        }
        public async Task<Room> GetRoomById(int? Id)
        {
            var room = await _context.Room
                .Where(r => r.Id== Id)
                .Include(r => r.Hotel)
                .Include(r => r.RoomPrices)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync();
            return room;
        }
        public async Task<bool> RoomExisted(string numberRoom)
        {
            var existed = await _context.Room.Where(r => r.RoomNumber.ToLower() == numberRoom.ToLower()).FirstOrDefaultAsync();
            return existed != null;
        }

        public async Task<bool> Save()
        {
            var r = await _context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<IEnumerable<Room>> SearchRoom(bool isAvailable)
        {
            var rooms = await _context.Room.Where(r => r.IsAvailable == isAvailable).ToListAsync();
            return rooms;

        }

        public async Task<bool> UpdateRoom(Room room)
        {
            _context.Room.Update(room);
            return await Save();
        }
    }
}
