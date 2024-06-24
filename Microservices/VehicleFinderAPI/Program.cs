using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using VehicleFinderAPI.Services;
using Algolia.Search.Clients;
using VehicleFinderAPI.DataSeederScript;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});
builder.Services.AddScoped<VehicleService>();

builder.Services.AddSingleton<ISearchClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new SearchClient(config["Algolia:ApplicationId"], config["Algolia:ApiKey"]);
});

builder.Services.AddSingleton<AlgoliaIndexService>();
builder.Services.AddScoped<DataSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

// Run data seeder
using (var scope = app.Services.CreateScope())
{
    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await dataSeeder.SeedAsync();
}

app.Run();
