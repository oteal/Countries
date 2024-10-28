using CountriesAPI.Models;
using System.Text.Json;

namespace CountriesAPI.Persistence {
	public class CountryPersistence {
		private static List<Country> countries;

		static CountryPersistence() {
			var jsonString = File.ReadAllText($"{AppContext.BaseDirectory}/Persistence/Mocks/CountriesMock.json");

			countries = JsonSerializer.Deserialize<List<Country>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
		}
		public List<Country> GetCountries() {
			return countries;
		}
		public Country GetCountry(int id) {
			return countries.Where(m => m.Id == id).FirstOrDefault();
		}
		public bool Exists(string name) {
			return countries.Any(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
		}
		public bool Add(Country country) {
			countries.Add(country);
			return true;
		}
	}
}
