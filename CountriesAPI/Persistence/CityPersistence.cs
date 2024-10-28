using CountriesAPI.Models;
using System.Text.Json;

namespace CountriesAPI.Persistence {
	public class CityPersistence {
		private static List<City> cities;

		static CityPersistence() {

			var jsonString = File.ReadAllText($"{AppContext.BaseDirectory}/Persistence/Mocks/CitiesMock.json");

			cities = JsonSerializer.Deserialize<List<City>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}
		public List<City> GetCities() {
			return cities;
		}
		public City GetCity(int id) {
			return cities.Where(m => m.Id == id).FirstOrDefault();
		}
		public bool Add(City city) {
			cities.Add(city);
			return true;
		}

		public bool UpdatePopulation(int id, long population) {
			var city = GetCity(id);

			if(city != null) {
				city.Population = population;

				return true;
			}
			else {
				return false;
			}
		}


		internal IEnumerable<City> GetCountryCities(int countryId) {
			return cities.Where(m => m.CountryId == countryId);
		}
	}
}
