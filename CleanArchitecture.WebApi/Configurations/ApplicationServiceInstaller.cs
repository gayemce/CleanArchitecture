
using CleanArchitecture.Application.Behaviors;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.WebApi.Configurations;

public sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        // AddMediatR - AssemblyReference
       services.AddMediatR(configure =>
            configure.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly));

        // ValidationBehavior için yaşam süresi oluşturuldu.
       services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // FluentValidation - AssemblyReference
       services.AddValidatorsFromAssembly(typeof
            (CleanArchitecture.Application.AssemblyReference).Assembly);
    }
}
