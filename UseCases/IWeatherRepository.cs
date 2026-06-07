// ============================================================================
// LAYER 2 — Application Business Rules (Use Cases)
// ============================================================================
// IWeatherRepository is the CONTRACT for data access. Notice that this
// interface lives in the inner layer, but the class that implements it
// (WeatherRepository) lives in the outer Frameworks layer.
//
// This is the heart of the Dependency Rule: the inner layer OWNS the
// interface, the outer layer OBEYS it. That lets us swap the storage
// (hardcoded values today, a real database tomorrow) without touching
// any business logic.
using WeatherAPI.Models;

namespace WeatherAPI.UseCases
{
	public interface IWeatherRepository
	{
		Temperature GetBySeason(string season);
	}
}