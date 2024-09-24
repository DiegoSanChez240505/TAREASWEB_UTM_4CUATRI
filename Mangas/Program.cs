using mangas.Services.Features.Mangas;
using mangas.Infraestructure.Repositories;
using Mangas.Services.MappingsM;

var builder = WebApplication.CreateBuilder(args);

// Registrar el servicio y el repositorio solo una vez
builder.Services.AddScoped<MangaService>();
builder.Services.AddTransient<MangaRepository>();

// Añadir controladores y Swagger para la API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ResponseMappingProfile).Assembly);

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
