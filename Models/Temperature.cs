// ============================================================================
// LAYER 1 — Enterprise Business Rules (the innermost layer)
// ============================================================================
// Temperature is an "entity": a plain C# class with NO framework dependencies
// (no ASP.NET, no database library, no using directives at all). Because it
// depends on nothing, every other layer is allowed to depend on it.
//
// It maps directly to a database table with four columns:
// Id, Season, MinTemp, MaxTemp.
namespace WeatherAPI.Models
{
	public class Temperature
	{
		public int Id { get; set; }
		// 'required' forces every Temperature to be given a Season when created,
		// which satisfies the non-nullable string and removes the warning.
		public required string Season { get; set; }
		public int MinTemp { get; set; }
		public int MaxTemp { get; set; }
	}
}