// ============================================================================
// LAYER 2 — Application Business Rules (Use Cases)
// ============================================================================
// IWeatherService is the CONTRACT for our business logic.
//
// Important design choice: GetTemperature returns `int`, NOT `Temperature`.
// We deliberately do not hand the entity out to the outer world. The outer
// layers only need the final number, so we keep the entity private to the
// inner layers. This is "don't leak your domain model".
namespace WeatherAPI.UseCases
{
	public interface IWeatherService
	{
		int GetTemperature();
	}
}