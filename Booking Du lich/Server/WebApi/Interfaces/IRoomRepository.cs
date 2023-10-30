using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IRoomRepository
    {
        public Task<bool> Save();
        public Task<bool> AddRoom(Room room);
        public Task<ICollection<Room>> GetAllRooms();
        public Task<IEnumerable<Room>> SearchRoomOfHotel(int? HotelId);

        public Task<Room> GetRoomById(int? Id);

        public Task<bool> DeleteRoom(Room room);

        public Task<bool> RoomExisted(string numberRoom);

        public Task<IEnumerable<Room>> SearchRoom(bool isAvailable);

        public Task<bool> UpdateRoom(Room room);
    }
}
