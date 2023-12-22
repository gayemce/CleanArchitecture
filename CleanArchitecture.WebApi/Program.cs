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

// ICarService instance t�ret�lmek istendi�inde CarService'in intanceni t�ret. - Dependency Injection
builder.Services.AddScoped<ICarService, CarService>();

// IAuthService instance t�ret�lmek istendi�inde AuthService'in intanceni t�ret. - Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IMailService, MailService>();

// ExceptionMiddleware i�in ya�am s�resi, instance olu�turur.
builder.Services.AddTransient<ExceptionMiddleware>();

// IUnitOfWork i�in ya�am s�resi, instance olu�turur.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

// ICarRepository instance t�ret�lmek istendi�inde CarRepository'in intanceni t�ret. - Dependency Injection
builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

// Class� olu�turup, configure methodunu tetikleyerek JwtOptionsa "Jwt" de�eri atan�r.
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

//Authentication - Json Web Token
builder.Services.AddAuthentication().AddJwtBearer();
//Authorization
builder.Services.AddAuthorization();

// AutoMapper Persistance katman�ndaki otomatik kendi yap�s�n� bulur.
builder.Services.AddAutoMapper(typeof
    (CleanArchitecture.Persistance.AssemblyReference).Assembly);

// Connection bilgisi app.setting.json da eklendi.
string connectionString = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
// User'�n AppDbContext ile ba�l� oldu�unu belirtmek i�in - Identitiy Password k�sm�n�n �zelle�tirilmesi
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

// ValidationBehavior i�in ya�am s�resi olu�turuldu.
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
