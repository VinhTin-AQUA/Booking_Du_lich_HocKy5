//using Booking.Data;
//using Booking.Interfaces;
//using Booking.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Booking.Repositories
//{
//    public class TouristAttractionRepository : ITouristAttraction
//    {
//        private readonly BookingContext context;
//        public TouristAttractionRepository(BookingContext context) { this.context = context; }
//        public async Task<bool> AddTouristAttraction(TouristAttraction touristAttraction)
//        {
//            context.TouristAttractions.Add(touristAttraction);
//            return await Save();
//        }

//        public async Task<bool> Delete(TouristAttraction touristAttraction)
//        {
//            context.TouristAttractions.Remove(touristAttraction);
//            return await Save();
//        }

//        public async Task<IEnumerable<TouristAttraction>> GetAllTouristAttraction(string searchString = "")
//        {
//            var touristattractions = await context.TouristAttractions.ToListAsync();
//            return touristattractions;
//        }

//        public async Task<TouristAttraction> GetTouristAttractionById(int? id)
//        {
//            var tAttraction = await context.TouristAttractions.Where(ta => ta.TouristAttractionId == id).FirstOrDefaultAsync();
//            return tAttraction;
//        }

//        public async Task<bool> Save()
//        {
//            var r = await context.SaveChangesAsync();
//            return r > 0;
//        }

//        public async Task<IEnumerable<TouristAttraction>> SearchTouristAttraction(string searchString)
//        {
//            var tAttraction = await context.TouristAttractions.Where(ta => ta.TouristAttractionName.ToLower().Contains(searchString.ToLower())).ToListAsync();
//            return tAttraction;
//        }

//        public async Task<bool> TouristAttractionExisted(string name)
//        {
//            var tAttraction = await context.TouristAttractions.Where(ta => ta.TouristAttractionName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
//            return tAttraction != null;
//        }

//        public async Task<bool> UpdateCity(TouristAttraction touristAttraction)
//        {
//            context.TouristAttractions.Update(touristAttraction);
//            return await Save();
//        }
//    }
//}
