
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.WebApi.Middleware;
using GenericRepository;

namespace CleanArchitecture.WebApi.Configurations;

public sealed class PersistanceDIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        // ICarService instance türetülmek istendiğinde CarService'in intanceni türet. - Dependency Injection
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRoleService, UserRoleService>();

        // ExceptionMiddleware için yaşam süresi, instance oluşturur.
        services.AddTransient<ExceptionMiddleware>();
        // IUnitOfWork için yaşam süresi, instance oluşturur.
        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
        // ICarRepository instance türetülmek istendiğinde CarRepository'in intanceni türet. - Dependency Injection
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
    }
}
