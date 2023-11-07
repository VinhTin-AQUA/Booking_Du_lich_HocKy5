using Bogus;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Seeds
{
    public static class SeedRoom
    {
        public static async Task SeedRoomAsync(ApplicationDbContext context)
        {
            Faker<Room> f_room = new Faker<Room>();
            Random random = new Random();

            f_room.RuleFor(r => r.RoomNumber, f => f.Lorem.Letter() + f.Random.Int(111, 999).ToString());
            f_room.RuleFor(r => r.RoomName, f => f.Company.CompanyName());
            f_room.RuleFor(r => r.Description, f => f.Lorem.Paragraphs(3,4));
            f_room.RuleFor(r => r.IsAvailable, f => true);

            LinkedList<RoomType> roomTypes = new LinkedList<RoomType>();
            roomTypes.AddLast(new RoomType { RoomTypeName = "Phòng 1 người" });
            roomTypes.AddLast(new RoomType { RoomTypeName = "Phòng 3 người" });
            roomTypes.AddLast(new RoomType { RoomTypeName = "Phòng 4 người" });

            LinkedList<Room> rooms = new LinkedList<Room>();

            for (int i = 1; i <= 10; i++) 
            {
                for (int j = 1; j <= 5; j++)
                {
                    Room room = f_room.Generate();
                    room.PhotoPath = $"/hotels/{i}/{j}";
                    room.HotelId = i;
                    room.RoomTypeId = random.Next(1,4);
                    rooms.AddLast(room);
                }
            }

            if (await context.RoomType.AnyAsync() == false)
            {
                await context.RoomType.AddRangeAsync(roomTypes);
                await context.SaveChangesAsync();
            }
            await context.Room.AddRangeAsync(rooms);
            await context.SaveChangesAsync();
        }
    }
}
