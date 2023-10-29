using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi1.Data;

namespace WebApi.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ApplicationDbContext context;
        public TourRepository(ApplicationDbContext context)
        {
            this.context = context;
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

        public async Task<ICollection<Tour>> GetAllTours()
        {
            var tours = await context.Tour.ToListAsync();
            return tours;
        }

        public async Task<Tour> GetTourById(int? id)
        {
            var tour = await context.Tour.Where(t => t.TourId == id).SingleOrDefaultAsync();
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
    }
}
