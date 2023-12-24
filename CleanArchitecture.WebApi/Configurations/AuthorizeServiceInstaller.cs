namespace CleanArchitecture.WebApi.Configurations;

public sealed class AuthorizeServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        //Authentication - Json Web Token
        services.AddAuthentication().AddJwtBearer();
        //Authorization
        services.AddAuthorization();
    }
}
