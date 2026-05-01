using GtMotive.Microservice.Api;
using GtMotive.Microservice.Application;
using GtMotive.Microservice.Infrastructure;
using GtMotive.Microservice.Infrastructure.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;

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

// Only run migrations if not in Testing environment
if (!app.Environment.IsEnvironment("Testing"))
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();
        try
        {
            db.Database.Migrate(); // Aplica migraciones pendientes
        }
        catch
        {
            // Log migration errors but don't fail startup
        }
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

//For Test
public partial class Program { }
