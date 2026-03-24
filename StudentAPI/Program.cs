using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Database;
using StudentAPI.DTO;
using StudentAPI.Helper;
using StudentAPI.Mapping;
using StudentAPI.Middleware;
using StudentAPI.Model;
using StudentAPI.Repository;
using StudentAPI.Repository.Interface;
using StudentAPI.Repository.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ApplicationSetting"));

// services scope 
// 1. AddSingleton, 2.AddTransient, 3.AddScoped
//builder.Services.AddScoped<IRepository<Student>, StudentService>();
builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<Student>), typeof(StudentService));

//builder.Services.AddTransient<IEmailService, EmailServices>();

// Automapper services 
builder.Services.AddAutoMapper(typeof(MappingProfile));

// add this service for fluent validation
// Register FluentValidation
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<StudentCustomFluentValidation>();

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

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();   // Connect controller route with request pipeline.

app.Run();   // Finally run the application.
