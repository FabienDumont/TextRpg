using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TextRpg.Application.Repositories;
using TextRpg.Infrastructure.EfRepositories;
using TextRpg.Infrastructure.JsonRepositories;

namespace TextRpg.Infrastructure;

public static class ServiceCollectionExtensions
{
  #region Methods

  public static void AddInfrastructureEf(this IServiceCollection services, string databasePath)
  {
    var connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databasePath)};";
    services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connectionString));
    services.AddScoped<ITraitRepository, TraitRepository>();
    services.AddScoped<IGreetingRepository, GreetingRepository>();
    services.AddScoped<IGameSaveRepository, GameSaveJsonRepository>();
  }

  #endregion
}
