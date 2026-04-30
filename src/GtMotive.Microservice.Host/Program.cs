using GtMotive.Microservice.Api;
using GtMotive.Microservice.Application;
using GtMotive.Microservice.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger/OpenAPI support
builder.Services.AddSwaggerGen();

// Register application and infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationCore();
builder.Services.AddVehicleApi();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
