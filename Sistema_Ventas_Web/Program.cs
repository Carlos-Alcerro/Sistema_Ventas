using DatosCapa.DataContext;
using Microsoft.EntityFrameworkCore;
using NegocioCapa.Helpers;
using NegocioCapa.Implementacion;
using NegocioCapa.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SistemaPosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddSingleton<TokenHelper>();
builder.Services.AddScoped<IUsuario, LogicaUsuario>();
builder.Services.AddScoped<IProducto, LogicaProducto>();
builder.Services.AddScoped<IDireccion, LogicaDireccion>();
builder.Services.AddScoped<IPedido, LogicaPedido>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
