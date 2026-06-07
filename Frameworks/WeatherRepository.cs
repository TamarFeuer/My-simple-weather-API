// ============================================================================
// LAYER 4 — Frameworks & Drivers (the outermost layer)
// ============================================================================
// WeatherRepository is the ONLY class that touches "storage". Today storage
// is just hardcoded Netherlands temperature ranges per season; tomorrow it
// could be SQL, a file, or a web API. Because it only implements the
// IWeatherRepository contract from Layer 2, swapping it out would not change
// a single line in the inner layers.
//
// This is intentionally the MOST changeable / least stable class in the
// project — the volatile details live as far out as possible.
using WeatherAPI.Models;
using WeatherAPI.UseCases;

namespace WeatherAPI.Frameworks
{
	public class WeatherRepository : IWeatherRepository
	{
		public Temperature GetBySeason(string season)
		{
			return season switch
			{
				"Summer" => new Temperature { Season = "Summer", MinTemp = 17, MaxTemp = 25 },
				"Autumn" => new Temperature { Season = "Autumn", MinTemp = 8,  MaxTemp = 14 },
				"Spring" => new Temperature { Season = "Spring", MinTemp = 8,  MaxTemp = 15 },
				_        => new Temperature { Season = "Winter", MinTemp = 2,  MaxTemp = 8  }
			};
		}
	}
}