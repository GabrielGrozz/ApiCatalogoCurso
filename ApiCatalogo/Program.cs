using ApiCatalogo.Context;
using ApiCatalogo.Logging;
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

//registro do nosso provbedor de loging
builder.Logging.AddProvider(new CustomLoggingProvider(new CustomLoggingProviderConfiguration { LogLevel = LogLevel.Information}));

//registrandop o serviço para a conexão com o banco de dados
var MySqlConnection = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDbContext>(op =>
{
    op.UseMySql(MySqlConnection, ServerVersion.AutoDetect(MySqlConnection));
});
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
