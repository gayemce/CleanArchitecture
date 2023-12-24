
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchitecture.WebApi.Configurations;

public class PersistanceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        // AutoMapper Persistance katmanındaki otomatik kendi yapısını bulur.
        services.AddAutoMapper(typeof
            (CleanArchitecture.Persistance.AssemblyReference).Assembly);

        // Connection bilgisi app.setting.json da eklendi.
        string connectionString = configuration.GetConnectionString("SqlServer");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        // User'ın AppDbContext ile bağlı olduğunu belirtmek için - Identitiy Password kısmının özelleştirilmesi
        services.AddIdentity<User, Role>(options =>
        {
            //options.Password.RequireNonAlphanumeric = false;
            //options.Password.RequiredLength = 1;
            //options.Password.RequireUppercase = false;

        }).AddEntityFrameworkStores<AppDbContext>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MSSqlServer(
            connectionString: connectionString,
            tableName: "Logs",
            autoCreateSqlTable: true)
            .CreateLogger();

        host.UseSerilog();
    }
}
