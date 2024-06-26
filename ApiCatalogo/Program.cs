using ApiCatalogo.Context;
using ApiCatalogo.DTOs.Mapping;
using ApiCatalogo.Logging;
using ApiCatalogo.Repository;
using ApiCatalogo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(op =>
{
    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registrandop o servi�o para a conex�o com o banco de dados
var MySqlConnection = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDbContext>(op =>
{
    op.UseMySql(MySqlConnection, ServerVersion.AutoDetect(MySqlConnection));
});

//adicionando no container DI
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//informa para o automapper quais tipos da aplicacao possui perfis de mapeamento
builder.Services.AddAutoMapper(typeof(ProductDTOMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
