using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cargamos el archivo de configuracion
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile("ocelot.json");

// Definir la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Configurar autenticación con JWT
builder.Services.AddAuthentication("Keycloak")
    .AddJwtBearer("Keycloak", options =>
    {
        options.Authority = "http://localhost:8080/realms/SuperTienda";
        options.Audience = "OcelotClient";
        options.RequireHttpsMetadata = false; // Solo para entornos de desarrollo

    });

// Agregamos el servicio de ocelot
builder.Services.AddOcelot();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar la política CORS
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.UseAuthentication();

// Usamos el servicio de ocelot.
await app.UseOcelot();

app.Run();
