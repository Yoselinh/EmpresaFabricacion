using System.Text.Json.Serialization;
using CasoPropuesto_5.Models;
using CasoPropuesto_5.Repository;
using CasoPropuesto_5.Repository.Implements;
using CasoPropuesto_5.Servicies;
using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Empresa de Fabricación - Caso 5",
        Version = "v1",
        Description = "API de gestión de producción, calidad, inventarios, proveedores e informes."
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IOrdenesProduccionRepository, OrdenesProduccionRepository>();
builder.Services.AddScoped<IInspeccionesCalidadRepository, InspeccionesCalidadRepository>();
builder.Services.AddScoped<IMateriasPrimaRepository, MateriasPrimaRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();

builder.Services.AddScoped<IProduccionService, ProduccionService>();
builder.Services.AddScoped<ICalidadService, CalidadService>();
builder.Services.AddScoped<IInventarioService, InventarioService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IInformeService, InformeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
