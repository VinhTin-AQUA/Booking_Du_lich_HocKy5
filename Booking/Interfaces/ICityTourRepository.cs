using Booking.Models;

namespace Booking.Interfaces
{
	public interface ICityTourRepository
	{
		public Task<bool> Save();
		public Task<bool> AddCityTour(CityTour cityTour);
		public Task<CityTour?> GetCityTourByTourId(int tourId);
		public Task<CityTour?> GetCityTourByCityId(int cityId);
		public Task<bool> DeleteCityTour(CityTour cityTour);
		public Task<bool> UpdateCityTour(CityTour cityTour);
	}
}
