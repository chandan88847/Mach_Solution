using AuthenticationAPI.Data;
using AuthenticationAPI.Services;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add services to the container.
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME"); ;
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); ;
var connectionstring = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";

builder.Services.AddDbContext<UserDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSqlServer(connectionstring,
                        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
});
builder.Services.AddHttpClient<InterServiceCalls>(client =>
{
    client.BaseAddress = new Uri("http://identity-service/");
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});
builder.Services.AddScoped<InterServiceCalls>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
