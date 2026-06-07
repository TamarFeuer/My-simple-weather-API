// ============================================================================
// LAYER 3 — Interface Adapters
// ============================================================================
// WeatherService implements the IWeatherService contract. It is where the
// actual business logic lives: figure out the current season, ask the
// repository for that season's range, and pick a random value inside it.
//
// Note the constructor: it receives an IWeatherRepository (the INTERFACE),
// never the concrete WeatherRepository. This is "constructor injection".
// The class has no idea where the data really comes from, so it stays
// decoupled from the storage details — dependencies point INWARD only.
using WeatherAPI.Models;    // needed for the Temperature entity used below
using WeatherAPI.UseCases;

namespace WeatherAPI.InterfaceAdapters
{
	public class WeatherService : IWeatherService
	{
		private readonly IWeatherRepository _repository;
		private readonly Random _random;

		// The repository is "injected" from outside. We depend on the
		// abstraction (IWeatherRepository), not a specific implementation.
		public WeatherService(IWeatherRepository repository)
		{
			_repository = repository;
			_random = new Random();
		}

		public int GetTemperature()
		{
			string season = GetCurrentSeason();
			// Ask the contract for the range; we don't know or care that the
			// data is currently hardcoded behind WeatherRepository.
			Temperature temp = _repository.GetBySeason(season);
			// Max + 1 because Random.Next's upper bound is EXCLUSIVE.
			return _random.Next(temp.MinTemp, temp.MaxTemp + 1);
		}

		// Business rule: map the current calendar month to a season.
		private string GetCurrentSeason()
		{
			int month = DateTime.Now.Month;
			if (month >= 6 && month <= 8) return "Summer";
			if (month >= 9 && month <= 11) return "Autumn";
			if (month >= 3 && month <= 5) return "Spring";
			return "Winter";
		}
	}
}