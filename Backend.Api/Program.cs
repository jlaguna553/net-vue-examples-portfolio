using Backend.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios al contenedor de dependencias
builder.Services.AddControllers();
builder.Services.AddScoped<ProductService>();

// 2. Configurar CORS para permitir solicitudes del frontend Vue
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 3. Habilitar Middleware
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();