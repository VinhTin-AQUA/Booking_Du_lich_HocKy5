using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Booking.Interfaces;
using Booking.Models;
using Booking.Data;

namespace Booking.Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly BookingContext context;

        public RoomTypeRepository(BookingContext context) {
            this.context = context;
        }
        public async Task<bool> AddRoomType(RoomType roomType)
        {
            context.RoomType.Add(roomType);
            return await Save();
        }

        public async Task<bool> DeleteRoomType(RoomType roomType)
        {
            if (roomType == null)
            {
                return false;
            }
            context.RoomType.Remove(roomType);
            return await Save();
        }

        public async Task<ICollection<RoomType>> GetAllRoomTypes()
        {
            var roomTypes = await context.RoomType.ToListAsync();
            return roomTypes;
        }

        public async Task<RoomType> GetRoomTypeById(int? id)
        {
            var roomType = await context.RoomType
                 .Where(rt => rt.RoomTypeId == id)
                 .FirstOrDefaultAsync();
            return roomType;
        }

        public async Task<RoomType> GetRoomTypeByName(string name)
        {
            var roomType = await context.RoomType
                .Where(rt => rt.RoomTypeName == name)
                .FirstOrDefaultAsync();
            return roomType;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<IEnumerable<RoomType>> SearchRoomType(string roomTypeName)
        {
            var roomTypes = await context.RoomType.Where(rt => rt.RoomTypeName.ToLower().Contains(roomTypeName.ToLower())).ToListAsync();
            return roomTypes;
        }

        public async Task<bool> UpdateRoomType(RoomType roomType)
        {
            context.RoomType.Update(roomType);
            return await Save();
        }
    }
}
