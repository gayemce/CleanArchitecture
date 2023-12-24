using CleanArchitecture.WebApi.Configurations;
using CleanArchitecture.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// InstallServices methodu çaðrýlarak implement yapýlan listeyi elde edip her birinin Install methodunu çalýþtýrýr.
builder.Services
    .InstallServices(
    builder.Configuration,
    builder.Host,
    typeof(IServiceInstaller).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
