using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controllers
builder.Services.AddControllers();

// AutoMapper: registra todos los perfiles automáticamente desde cualquier clase base
builder.Services.AddAutoMapper(typeof(MapperProfile));

// FluentValidation: registra todos los validadores de este ensamblado
builder.Services.AddValidatorsFromAssemblyContaining<EstudianteCreateValidator>();

// Servicios
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();

// Repositorios
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<IProfesorRepository, ProfesorRepository>();

// CORS para permitir peticiones desde Angular u otro cliente web
var MyCorsPolicy = "AllowAngular";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seguridad y CORS
app.UseHttpsRedirection();
app.UseCors(MyCorsPolicy);

// Rutas
app.UseAuthorization();
app.MapControllers();

// Solo para pruebas: puede ser eliminado luego
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm",
        "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        )).ToArray();

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

// DTO temporal solo para la ruta de prueba
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}