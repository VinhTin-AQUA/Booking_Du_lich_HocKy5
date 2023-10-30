using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IRoomPriceRepository
    {
        public Task<bool> Save();
        public Task<bool> AddRoomPrice(RoomPrice roomPrice);

        public Task<ICollection<RoomPrice>> GetAllRoomPrices();
        public Task<RoomPrice> GetRoomPriceByPrice(double price);

        public Task<RoomPrice> GetRoomPriceById(int? id);
        public Task<RoomPrice> GetRoomPriceByID(int? id , DateTime? validFrom);
        public Task<bool> DeleteRoomPrice(RoomPrice roomPrice);


        //public Task<IEnumerable<RoomPrice>> SearchRoomType(decimal price);

        public Task<bool> UpdateRoomPrice(RoomPrice roomPrice);
    }
}
