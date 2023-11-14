using Booking.Models;

namespace Booking.Interfaces
{
    public interface ITourTypeRepository
    {
        public Task<bool> Save();
        public Task<TourType> GetTourTypeById(int? id);
        public Task<bool> AddTourType(TourType type);
        public Task<bool> TourTypeExisted(string name);
        public Task<IEnumerable<TourType>> GetAllTourType();
        public Task<bool> Delete(TourType type);
        public Task<bool> UpdateTourType(TourType type);
    }
}
