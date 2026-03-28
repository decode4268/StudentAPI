using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentAPI.Database;
using StudentAPI.DTO;
using StudentAPI.Helper;
using StudentAPI.Mapping;
using StudentAPI.Middleware;
using StudentAPI.Model;
using StudentAPI.Repository;
using StudentAPI.Repository.Interface;
using StudentAPI.Repository.Services;
using System.Text;

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
builder.Services.AddScoped<ITokenService, TokenService>();


//TokenService: ITokenService

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

builder.Services.AddAuthentication(options =>
{
    // Default scheme used for authentication
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    // Default scheme used for challenge (when authentication fails)
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    // Default scheme used for sign-in
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:IssuerSigningKey"]))
    };
});

// for passing token in header in swagger..
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add JWT Bearer Authorization to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
});


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
