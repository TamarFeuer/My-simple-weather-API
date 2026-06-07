// ============================================================================
// THE COMPOSITION ROOT
// ============================================================================
// Program.cs is the one special place in Clean Architecture where the
// Dependency Rule is deliberately relaxed. Everywhere else, classes depend
// only on the INTERFACES from the inner layers. But SOMEONE has to decide
// which concrete class implements each interface — and that decision is made
// here, at the outermost edge of the app, when it starts up.
//
// That is why this file is allowed to reference every layer at once.
using WeatherAPI.UseCases;          // the interfaces (Layer 2)
using WeatherAPI.InterfaceAdapters; // WeatherService (Layer 3)
using WeatherAPI.Frameworks;        // WeatherRepository + endpoints (Layer 4)

// Create the "builder": a configuration/prep object used to set the app up
// BEFORE it starts running. CreateBuilder comes pre-loaded with sensible
// defaults, so out of the box it already wires up:
//   - the web server (Kestrel — note "server: Kestrel" in the response headers)
//   - configuration (reads appsettings.json / appsettings.Development.json)
//   - logging (the "info: Microsoft.Hosting.Lifetime" lines in the terminal)
//   - the dependency injection container (builder.Services — used below to
//     register IWeatherService, IWeatherRepository, controllers, and Swagger)
// 'args' forwards command-line arguments (e.g. --urls) into that setup.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Register the MVC controllers (this is what makes WeatherEndpoint reachable).
builder.Services.AddControllers();

// Swagger: generates an interactive web page for testing the API by hand.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- The actual wiring of the architecture ---------------------------------
// "When something asks for IWeatherRepository, give it a WeatherRepository."
// "When something asks for IWeatherService, give it a WeatherService."
// ASP.NET's container then builds the whole chain for us automatically:
//   WeatherEndpoint <- IWeatherService <- IWeatherRepository
//
// AddScoped = create one instance per HTTP request (the standard choice for
// web APIs). The inner layers never see these lines — they only ever know
// about the interfaces.
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

WebApplication app = builder.Build();

// Only expose the Swagger UI while developing, never in production.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();   // hooks the controller routes into the request pipeline
app.Run();