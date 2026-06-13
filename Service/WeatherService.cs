// ============================================================================
// SERVICE - business logic
// ============================================================================
// WeatherService does the work: look up the chosen month's range and pick a
// random value inside it. It implements IWeatherService and uses
// IWeatherRepository to get data.
//
// Pure logic - no ASP.NET, no database. The repository arrives via the
// constructor (constructor injection), so it never knows where data comes from.
using WeatherAPI.Models;
using WeatherAPI.Repository;

namespace WeatherAPI.Service
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

		// Returns a random temperature in the chosen month's range, or null if
		// the month is unknown.
		public int? GetTemperature(string month)
		{
			Temperature? row = _repository.GetByMonth(month);
			if (row is null)
				return null;
			// Max + 1 because Random.Next's upper bound is EXCLUSIVE.
			return _random.Next(row.MinTemp, row.MaxTemp + 1);
		}
	}
}
