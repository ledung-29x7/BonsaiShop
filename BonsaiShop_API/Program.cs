using BonsaiShop_API;
using BonsaiShop_API.DALL.Repositories;
using BonsaiShop_API.DALL.RepositoriesImplement;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
           .Build();
builder.Services.AddDbContext<BonsaiDbcontext>(options => options.UseSqlServer(configuration.GetConnectionString("Bonsai")));
builder.Services.AddScoped<IPlantsRepository , PlantsRepository>();
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
