using Microsoft.EntityFrameworkCore;
using StudentAPI.Database;
using StudentAPI.Model;
using StudentAPI.Repository.Interface;
using StudentAPI.Repository.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// services scope 
// 1. AddSingleton, 2.AddTransient, 3.AddScoped
builder.Services.AddScoped<IRepository, StudentService>();
builder.Services.AddScoped<ApplicationDbContext>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();   // Connect controller route with request pipeline.

app.Run();   // Finally run the application.
