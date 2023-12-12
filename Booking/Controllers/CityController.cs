using Booking.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Controllers
{
	public class CityController : Controller
	{
		private readonly ICityRepository cityRepository;

		public CityController(ICityRepository cityRepository)
        {
			this.cityRepository = cityRepository;
		}

		public async Task<IActionResult> GetAllCities()
		{
			var cities = await cityRepository.GetAllCities();
			var cityNames = cities.Select(city => city.Name).ToList();
			return Json(new { cityNames });
		}
    }
}
