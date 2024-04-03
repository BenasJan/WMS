// WMS - Workout management system

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WmsApi.Controllers;
using WmsApi.Database;
using WmsApi.Database.Repositories;
using WmsApi.Mappers;
using WmsApi.Middleware;
using WmsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ErrorHandlerMiddleware>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(@"Data Source =.\WmsDatabase.db"));

builder.Services
    .AddScoped(_ =>
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WorkoutsMappingProfile>();
        });
        var mapper = mapperConfiguration.CreateMapper();

        return mapper;
    })
    .AddScoped<WmsDatabase>()
    .AddScoped<WorkoutsRepository>()
    .AddScoped<WorkoutsService>()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapWorkouts()
    .UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
