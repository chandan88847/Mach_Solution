using Algolia.Search.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParkAPI.Data;
using ParkAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME"); ;
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); ;
var connectionstring = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";

builder.Services.AddDbContext<ParkDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSqlServer(connectionstring,
                        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Logging.AddConsole();
var logger = LoggerFactory.Create(logging => logging.AddConsole()).CreateLogger<Program>();
logger.LogInformation($"Connection string: {connectionstring}");

builder.Services.AddScoped<ParkService>();

builder.Services.AddSingleton<ISearchClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new SearchClient(config["Algolia:ApplicationId"], config["Algolia:ApiKey"]);
});

builder.Services.AddSingleton<AlgoliaIndexService>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ParkDbContext>();
    try
    {
        dbContext.Database.Migrate();
        logger.LogInformation("Database migrated successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}
// Configure the HTTP request pipeline.
app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
