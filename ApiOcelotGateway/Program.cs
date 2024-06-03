using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//adding josn file to configuration
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json",optional:false,reloadOnChange:true)
    .AddEnvironmentVariables();
//adds iconfiguration which reads environmental variables from configuration with specified prefix

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
await app.UseOcelot();


app.Run();
