using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public HotelRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> AddHotel(Hotel hotel)
        {
            context.Hotel.Add(hotel);
            return await Save();
        }

        public async Task<ICollection<Hotel>> GetAllHotels()
        {
            var hotels = await context.Hotel.ToListAsync();
            return hotels;
        }

        public async Task<Hotel> GetHotelById(int? id)
        {
            var hotel = await context.Hotel
                .Where(h => h.Id == id)
                .SingleOrDefaultAsync();
            return hotel;
        }
        public async Task<ICollection<Hotel>> GetHotelsOfAgent(string posterId)
        {
            var hotels = await context.Hotel
                .Include(h => h.City)
                .Where(h => h.PosterID == posterId)
                .ToListAsync();
            return hotels;
        }

        public async Task<IdentityResult> AdddAgent(ApplicationUser agent, string password)
        {
            var r = await userManager.CreateAsync(agent, password);
            return r;
        }

        public async Task<IdentityResult> DeleteAgent(ApplicationUser agent)
        {
            var r = await userManager.DeleteAsync(agent);
            return r;
        }

        public async Task<bool> DeleteHotel(Hotel hotel)
        {
            // delete hotel
            context.Hotel.Remove(hotel);
            return await Save();
        }

        public async Task<bool> UpdateHotel(Hotel hotel)
        {
            context.Hotel.Update(hotel);
            return await Save();
        }

        
    }
}
