using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Data;

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

        public async Task<ICollection<Hotel>> HotelNotApproved()
        {
            var hotels = await context.Hotel
                .Where(h => h.Approver == null)
                .Include(h => h.Poster)
                .ToListAsync();
            return hotels;
        }

        public async Task<ICollection<Hotel>> HotelApproved()
        {
            var hotels = await context.Hotel
                .Where(h => h.Approver != null)
                .Include(h => h.Poster)
                .Include(h => h.Approver)
                .ToListAsync();
            return hotels;
        }

        public async Task<Hotel> GetHotelById(int? id)
        {
            var hotel = await context.Hotel
                .Where(h => h.Id == id)
                .Include(h => h.Poster)
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

        public async Task<ICollection<Hotel>> GetHotelsInCity(int cityId)
        {
            var hotels = await context.Hotel
                .Where(h => h.CityId == cityId)
                .ToListAsync();
            return hotels;
        }

        public async Task<ICollection<ApplicationUser>> GetAgentHotels(int partnerId)
        {
            var users = await userManager.Users.Where(u => u.PartnerId == partnerId)
                .ToListAsync();

            return users;
        }
    } 
}
