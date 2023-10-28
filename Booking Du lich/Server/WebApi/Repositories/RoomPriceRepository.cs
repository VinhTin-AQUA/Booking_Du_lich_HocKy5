using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class RoomPriceRepository : IRoomPriceRepository
    {
        private readonly ApplicationDbContext context;

        public RoomPriceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddRoomPrice(RoomPrice roomPrice)
        {
            context.RoomPrices.Add(roomPrice);
            return await Save();
        }

        public async Task<bool> DeleteRoomPrice(RoomPrice roomPrice)
        {
            if (roomPrice == null)
            {
                return false;
            }
            context.RoomPrices.Remove(roomPrice);
            return await Save();
        }

        public async Task<ICollection<RoomPrice>> GetAllRoomPrices()
        {
            var roomPrices = await context.RoomPrices.ToListAsync();
            return roomPrices;
        }
        public async Task<RoomPrice> GetRoomPriceById(int? id)
        {
            var roomPrice = await context.RoomPrices
                 .Where(rt => rt.RoomId == id )
                 .FirstOrDefaultAsync();
            return roomPrice;
        }
        public async Task<RoomPrice> GetRoomPriceByID(int? id, DateTime? validFrom)
        {
            var roomPrice = await context.RoomPrices
                 .Where(rt => rt.RoomId == id && rt.ValidFrom == validFrom)
                 .FirstOrDefaultAsync();
            return roomPrice;
        }

        public async Task<RoomPrice> GetRoomPriceByPrice(decimal price)
        {
            var roomPrice = await context.RoomPrices
                .Where(rt => rt.Price == price)
                .FirstOrDefaultAsync();
            return roomPrice;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> UpdateRoomPrice(RoomPrice roomPrice)
        {
            context.RoomPrices.Update(roomPrice);
            return await Save();
        }
    }
}
