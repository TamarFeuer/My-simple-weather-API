// ============================================================================
// REPOSITORY - storage contract
// ============================================================================
// IMonthDataSource is the contract WeatherRepository uses to reach storage.
// Its drivers (SqlMonthDataSource, JsonMonthDataSource) implement it. This is
// what keeps WeatherRepository free of any file/JSON/database details and lets
// the storage be swapped behind the interface.
using WeatherAPI.Models;

namespace WeatherAPI.Repository
{
	public interface IMonthDataSource
	{
		// Returns every month's range as stored. WeatherRepository picks the one
		// it needs.
		IReadOnlyList<Temperature> GetAll();
	}
}
