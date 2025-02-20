using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar Entity Framework con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Application.Interfaces.IDhtSensorService, Application.Services.DhtSensorService>();
builder.Services.AddScoped<Domain.Interfaces.IDhtSensorRepository, Infrastructure.Repositories.DhtSensorRepository>();
builder.Services.AddScoped<Application.Interfaces.IEventService, Application.Services.EventService>();
builder.Services.AddScoped<Domain.Interfaces.IEventRepository, Infrastructure.Repositories.EventRepository>();
builder.Services.AddScoped<Application.Interfaces.IActuatorService, Application.Services.ActuatorService>();
builder.Services.AddScoped<Domain.Interfaces.IActuatorRepository, Infrastructure.Repositories.ActuatorRepository>();



// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowLocalhost4200");

app.UseAuthorization();
app.MapControllers();

app.Run();
