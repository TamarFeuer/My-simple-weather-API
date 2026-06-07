// ============================================================================
// LAYER 4 — Frameworks & Drivers (the outermost layer)
// ============================================================================
// WeatherEndpoint is tied to ASP.NET ([ApiController], ControllerBase). That
// framework coupling is fine HERE, because this is the outermost layer — the
// place where the web framework is allowed to leak in.
//
// Its only job is translation: receive the HTTP request, call the business
// logic through the IWeatherService contract, and shape the result into JSON.
// It contains NO business logic itself.
//
// The Dependency Rule in action:
//   WeatherEndpoint --> IWeatherService --> IWeatherRepository
// Every arrow points inward, toward the abstractions, never the reverse.
//
// Endpoint:  GET /api/weather/temperature
//   -> { "temperature": 22 }
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.UseCases;

namespace WeatherAPI.Frameworks
{
	[ApiController]
	[Route("api/weather")]
	public class WeatherEndpoint : ControllerBase
	{
		// Injected via the IWeatherService interface — the endpoint never
		// references the concrete WeatherService class.
		private readonly IWeatherService _service;

		public WeatherEndpoint(IWeatherService service)
		{
			_service = service;
		}

		// Handles: GET /api/weather/temperature
		[HttpGet("temperature")]
		public IActionResult GetTemperature()
		{
			int temperature = _service.GetTemperature();
			// Wrap the int in an anonymous object so the JSON comes out as
			// { "temperature": 22 } instead of just a bare number.
			return Ok(new { temperature });
		}
	}
}