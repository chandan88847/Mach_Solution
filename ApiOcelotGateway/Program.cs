using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Adding json file to configuration
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Adding CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()
          //  .WithOrigins("http://localhost:3000/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("Content-Disposition"); // Optional: Expose specific headers if needed
    });
});

// Adding Ocelot configuration
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Ensure CORS policy is applied before other middlewares
app.UseCors("AllowAllOrigins");

// Add a middleware to handle OPTIONS requests
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        return;
    }

    await next();
});

// Applying Ocelot middleware
await app.UseOcelot();

app.Run();
