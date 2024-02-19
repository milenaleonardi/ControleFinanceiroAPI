using ControleFinanceiro.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


// Instalar Pacotes

//dotnet add package Microsoft.EntityFrameworkCore --version 6.0.10
//dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.10
//dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.10

//dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 6.0.0
//dotnet add package AutoMapper --version 8.0.0
//dotnet add package MySql.Data.EntityFramework