
using BackEndSGTA.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Middleware / Pipeline
app.ConfigurePipeline();

app.Run();
