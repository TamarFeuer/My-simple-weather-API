// ============================================================================
// REPOSITORY - EF Core database context
// ============================================================================
// WeatherDbContext is EF Core's representation of the database.
//
// - DbSet<Temperature> Temperatures  =  the "Temperatures" TABLE.
// - Each Temperature object           =  one ROW.
// - The Id / Month / MinTemp / MaxTemp properties = the COLUMNS.
//
// OnModelCreating seeds the twelve months, so the database is never empty on
// first run. EF Core turns all of this into real SQL for us.
using Microsoft.EntityFrameworkCore;
using WeatherAPI.Models;

namespace WeatherAPI.Repository
{
	public class WeatherDbContext : DbContext
	{
		// The options (which database, which file) are injected from Program.cs;
		// the DbContext itself doesn't hardcode where the data lives.
		public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
			: base(options)
		{
		}

		// This property IS the table. Querying it generates "SELECT ... FROM Temperatures".
		public DbSet<Temperature> Temperatures => Set<Temperature>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Seed data: the same twelve ranges that live in months.json, now rows
			// in the table. HasData needs explicit primary-key values.
			modelBuilder.Entity<Temperature>().HasData(
				new Temperature { Id = 1,  Month = "January",   MinTemp = 1,  MaxTemp = 6  },
				new Temperature { Id = 2,  Month = "February",  MinTemp = 1,  MaxTemp = 7  },
				new Temperature { Id = 3,  Month = "March",     MinTemp = 3,  MaxTemp = 10 },
				new Temperature { Id = 4,  Month = "April",     MinTemp = 5,  MaxTemp = 14 },
				new Temperature { Id = 5,  Month = "May",       MinTemp = 9,  MaxTemp = 18 },
				new Temperature { Id = 6,  Month = "June",      MinTemp = 12, MaxTemp = 21 },
				new Temperature { Id = 7,  Month = "July",      MinTemp = 14, MaxTemp = 23 },
				new Temperature { Id = 8,  Month = "August",    MinTemp = 14, MaxTemp = 23 },
				new Temperature { Id = 9,  Month = "September", MinTemp = 11, MaxTemp = 19 },
				new Temperature { Id = 10, Month = "October",   MinTemp = 8,  MaxTemp = 15 },
				new Temperature { Id = 11, Month = "November",  MinTemp = 4,  MaxTemp = 10 },
				new Temperature { Id = 12, Month = "December",  MinTemp = 2,  MaxTemp = 7  }
			);
		}
	}
}
