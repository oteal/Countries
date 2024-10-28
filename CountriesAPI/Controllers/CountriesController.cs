using CountriesAPI.Models;
using CountriesAPI.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace CountriesAPI.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class CountriesController : ControllerBase {
		CountryPersistence _countryPersitence;
		CityPersistence _cityPersitence;
		public CountriesController() {
			_countryPersitence = new CountryPersistence();
			_cityPersitence = new CityPersistence();
		}

		[HttpGet, Route("List")]
		public IEnumerable<Country> CountriesList() {
			return _countryPersitence.GetCountries();
		}
		[HttpGet, Route("{id}")]
		public Country CountryDetails(int id) {
			return _countryPersitence.GetCountry(id);
		}
		[HttpPost, Route("Country")]
		public IActionResult CountryCreate([FromBody] Country country) {

			if(_countryPersitence.Exists(country.Name)) {
				return Unauthorized("Country with same name already exists");
			}
			if(_countryPersitence.Add(country)) {
				return Ok("Success!");
			}
			else {
				return Unauthorized();
			}
		}
		[HttpGet, Route("{countryId}/Cities")]
		public IActionResult GetCountryCities(int countryId) {
			if(_countryPersitence.GetCountry(countryId) == null) {
				return NotFound("Country not found");
			}

			return Ok(_cityPersitence.GetCountryCities(countryId));
		}
	}
}