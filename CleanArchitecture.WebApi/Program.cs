using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.WebApi.Middleware;
using CleanArchitecture.WebApi.OptionsSetup;
using CleanArcihtecture.Infrastructure.Authenticaton;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ICarService instance türetülmek istendiðinde CarService'in intanceni türet. - Dependency Injection
builder.Services.AddScoped<ICarService, CarService>();

// IAuthService instance türetülmek istendiðinde AuthService'in intanceni türet. - Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IMailService, MailService>();

// ExceptionMiddleware için yaþam süresi, instance oluþturur.
builder.Services.AddTransient<ExceptionMiddleware>();

// IUnitOfWork için yaþam süresi, instance oluþturur.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

// ICarRepository instance türetülmek istendiðinde CarRepository'in intanceni türet. - Dependency Injection
builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

// Classý oluþturup, configure methodunu tetikleyerek JwtOptionsa "Jwt" deðeri atanýr.
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

//Authentication - Json Web Token
builder.Services.AddAuthentication().AddJwtBearer();
//Authorization
builder.Services.AddAuthorization();

// AutoMapper Persistance katmanýndaki otomatik kendi yapýsýný bulur.
builder.Services.AddAutoMapper(typeof
    (CleanArchitecture.Persistance.AssemblyReference).Assembly);

// Connection bilgisi app.setting.json da eklendi.
string connectionString = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
// User'ýn AppDbContext ile baðlý olduðunu belirtmek için - Identitiy Password kýsmýnýn özelleþtirilmesi
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    //options.Password.RequireNonAlphanumeric = false;
    //options.Password.RequiredLength = 1;
    //options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<AppDbContext>();

// AssemblyReference
builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly);

// AddMediatR - AssemblyReference
builder.Services.AddMediatR(configure =>
    configure.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly));

// ValidationBehavior için yaþam süresi oluþturuldu.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// FluentValidation - AssemblyReference
builder.Services.AddValidatorsFromAssembly(typeof
    (CleanArchitecture.Application.AssemblyReference).Assembly);

builder.Services.AddEndpointsApiExplorer();

//Swagger ile Login olma
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
