using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IRoomTypeRepository
    {
        public Task<bool> Save();
        public Task<bool> AddRoomType(RoomType roomType);

        public Task<ICollection<RoomType>> GetAllRoomTypes();
        public Task<RoomType> GetRoomTypeByName(string? nameType);

        public Task<RoomType> GetRoomTypeById(int? id);
        public Task<bool> DeleteRoomType(RoomType roomType);

        
        public Task<IEnumerable<RoomType>> SearchRoomType(string roomTypeName);

        public Task<bool> UpdateRoomType(RoomType roomType);
    }
}
