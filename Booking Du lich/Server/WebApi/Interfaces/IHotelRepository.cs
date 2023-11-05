﻿using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IHotelRepository
    {
        public Task<bool> Save();
        public Task<bool> AddHotel(Hotel hotel);
        public Task<Hotel> GetHotelById(int? id);
        public Task<ICollection<Hotel>> GetAllHotels();

        public Task<ICollection<Hotel>> HotelNotApproved();

        public Task<ICollection<Hotel>> HotelApproved();
        public Task<IdentityResult> AdddAgent(ApplicationUser agent, string password);

        public Task<ICollection<Hotel>> GetHotelsOfAgent(string posterId);
        public Task<IdentityResult> DeleteAgent(ApplicationUser agent);
        public Task<bool> DeleteHotel(Hotel hotel);
        public Task<bool> UpdateHotel(Hotel hotel);
        public Task<ICollection<Hotel>> GetHotelsInCity(int cityId);
    }
}
