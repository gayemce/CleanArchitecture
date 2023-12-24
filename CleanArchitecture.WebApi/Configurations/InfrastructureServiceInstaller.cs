
using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.WebApi.OptionsSetup;
using CleanArcihtecture.Infrastructure.Authenticaton;

namespace CleanArchitecture.WebApi.Configurations;

public sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
       services.AddScoped<IJwtProvider, JwtProvider>();

        // Classı oluşturup, configure methodunu tetikleyerek JwtOptionsa "Jwt" değeri atanır.
       services.ConfigureOptions<JwtOptionsSetup>();
       services.ConfigureOptions<JwtBearerOptionsSetup>();
    }
}
