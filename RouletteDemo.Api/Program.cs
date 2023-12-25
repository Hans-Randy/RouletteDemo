using Microsoft.EntityFrameworkCore;
using RouletteDemo.Api.Data;
using RouletteDemo.Api.Interfaces;
using RouletteDemo.Api.Middlewares;
using RouletteDemo.Api.Models;
using RouletteDemo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IRouletteService, RouletteService>();
builder.Services.AddScoped<IRepository<Bet>, EntityRepository<Bet>>();
builder.Services.AddScoped<IRepository<Spin>, EntityRepository<Spin>>();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
