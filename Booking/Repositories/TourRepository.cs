﻿using Booking.Data;
using Booking.Interfaces;
using Booking.Models;
using Booking.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing.Printing;

namespace WebApi.Repositories
{

    public class TourRepository : ITourRepository
    {
        private readonly BookingContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICityRepository cityRepository;

        public TourRepository(BookingContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ICityRepository cityRepository)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.cityRepository = cityRepository;
        }
        public async Task<bool> AddTour(Tour tour)
        {
            context.Tour.Add(tour);
            return await Save();
        }

        public async Task<bool> DeleteTour(Tour tour)
        {
            context.Tour.Remove(tour);
            return await Save();
        }

        public async Task<ICollection<Tour>> GetAllTours(int currentPage, int pageSize)
        {
            if (currentPage == -1 && pageSize == -1)
            {
                var _tours = await context.Tour
                    .ToListAsync();
                return _tours;
            }

            if (currentPage == -1)
            {
                currentPage = 0;
            }
            var tours = await context.Tour
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return tours;
        }

        public async Task<ICollection<Tour>> SearchTour(string searchString)
        {
            var tours = await context.Tour
                .Where(t => t.TourName.ToLower().Contains(searchString.ToLower()))
                .ToListAsync();
            return tours;
        }

        public async Task<ICollection<Tour>> GetToursOfPoster(string posterId)
        {
            var list = await context.Tour
                .Where(t => t.PosterID == posterId)
                .ToListAsync();
            return list;
        }

        public async Task<Tour> GetTourById(int? id)
        {
            var tour = await context.Tour
                .Where(t => t.TourId == id)
                .Include(t => t.Approver)
                .Include(t => t.Poster)
                .SingleOrDefaultAsync();
            return tour;
        }

        public async Task<bool> Save()
        {
            var r = await context.SaveChangesAsync();
            return r > 0;
        }

        public async Task<bool> UpdateTour(Tour tour)
        {
            context.Tour.Update(tour);
            return await Save();
        }

        public async Task<bool> AddTypeToTour(TourCategory tourType, Tour tour)
        {
            tour.TourCategories.Add(tourType);
            context.Tour.Update(tour);
            return await Save();
        }


        public async Task<int> TotalAgent(string searchString = "")
        {
            int totalUser = 0;
            var users = await userManager.GetUsersInRoleAsync("AgentTour");
            if (string.IsNullOrEmpty(searchString))
            {
                totalUser = users.Count();
                return totalUser;
            }

            totalUser = users
                .Where(u => (u.FirstName.ToLower().Contains(searchString.ToLower()) || u.LastName.ToLower().Contains(searchString.ToLower())) && u.Email != SeedAdmin.Email)
                .Count();
            return totalUser;
        }


        public async Task<ICollection<AppUser>> GetAgentTours(int currentPage, int pageSize, string? searchString)
        {
            var users = await userManager.GetUsersInRoleAsync(SeedRole.AGENTTOUR_ROLE);
            if (string.IsNullOrEmpty(searchString))
            {
                var agentTours = users
                .Where(u => u.Email != SeedAdmin.Email)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToList();
                return agentTours;
            }

            var _agentTours = users
                .Where(u => (u.FirstName.ToLower().Contains(searchString.ToLower()) || u.LastName.ToLower().Contains(searchString.ToLower())) && u.Email != SeedAdmin.Email)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToList();

            return _agentTours.ToList();
        }

        public async Task<ICollection<Tour>> GetTourByCityName(string? cityName)
        {
            City cityTemp = await cityRepository.GetCityByName(cityName);

            if (cityTemp == null)
            {
                return new List<Tour>();
            }

            var tours = await context.City.Where(city => city.Id == cityTemp.Id)
                .Join(context.CityTour, c => c.Id, ct => ct.CityId, (c, ct) => new { c, ct })
                .Join(context.Tour, cct => cct.ct.TourId, t => t.TourId, (cct, t) => new { cct, t })
                .Select(ts => ts.t).ToListAsync();
            return tours;
        }

        public async Task<ICollection<Tour>> GetTourByCategory(int categoryId, int cityId)
        {
            var tours = from tour in context.Tour
                        join cityTour in context.CityTour on tour.TourId equals cityTour.TourId
                        join city in context.City on cityTour.CityId equals city.Id
                        join tourCategory in context.TourCategories on tour.TourId equals tourCategory.TourId
                        where tourCategory.CategoryId == categoryId && city.Id == cityId
                        select tour;
            return tours.ToList();
        }


        public async Task<double> GetMinPriceOfTour(Tour tour)
        {
            var prices = await (from pr in context.PackagePrices
                                join package in context.Packages on pr.PackageId equals package.PackageID
                                join t in context.Tour on package.TourID equals tour.TourId
                                where t.TourId == tour.TourId
                                select pr.AdultPrice).ToListAsync();

            if (prices.Any())
            {
                double price = prices.Min();
                return price;
            }
            return -1;
        }
    }
}
