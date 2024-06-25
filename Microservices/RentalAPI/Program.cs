using Microsoft.EntityFrameworkCore;
using RentalAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RentalDbContext>(options => 
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();