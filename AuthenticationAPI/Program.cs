using AuthenticationAPI.Data;
using AuthenticationAPI.Models;
using AuthenticationAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME"); ;
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); ;
var connectionstring = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSqlServer(connectionstring,
                         sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
});
builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit= false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
});


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

//builder.Services.AddHttpClient<ApplicationUserService>(client =>
//{
//    client.BaseAddress = new Uri("http://identity-service/");
//});

builder.Services.AddHttpClient<ApplicationUserService>();
builder.Services.AddScoped<ApplicationUserService>();


var app = builder.Build();
app.UseCors("AllowAllOrigins");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
