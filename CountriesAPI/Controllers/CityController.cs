using CountriesAPI.Models;
using CountriesAPI.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace CountriesAPI.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class CityController : ControllerBase {
		CityPersistence _cityPersitence;
		CountryPersistence _countryPersitence;
		public CityController() {
			_cityPersitence = new CityPersistence();
		}

		[HttpGet, Route("List")]
		public IEnumerable<City> CountriesList() {
			return _cityPersitence.GetCities();
		}
		[HttpGet, Route("{id}")]
		public City CityDetails([FromQuery] int id) {
			return _cityPersitence.GetCity(id);
		}
		[HttpPost, Route("City")]
		public IActionResult CityCreate([FromBody] City city) {

			if(_countryPersitence.GetCountry(city.CountryId) == null) {
				return NotFound("Country not found");
			}
			if(_cityPersitence.Add(city)) {
				return Ok("Success!");
			}
			else {
				return Unauthorized();
			}
		}
		[HttpPut, Route("{id}/Population")]
		public IActionResult UpdatePopulation(int id, [FromBody] long population) {
			if(_cityPersitence.UpdatePopulation(id, population)) {
				return Ok("Success!");
			}
			else {
				return Unauthorized();
			}
		}
	}
}