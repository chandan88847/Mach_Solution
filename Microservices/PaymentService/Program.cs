using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMvc().AddNewtonsoftJson();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
StripeConfiguration.ApiKey = "sk_test_51PKxtiSGyaisjdTZP6j76xQ0TxMH0MxwmuHbLkfP7wB8W9TOqZcyVofMBzwFqFa7tR2ZUEUdlrDOyVEuHRR2rvc100vCzpc6Ib";

app.UseRouting();
app.UseStaticFiles();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseAuthorization();

app.MapControllers();

app.Run();
