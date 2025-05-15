﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TextRpg.Application.Repositories;
using TextRpg.Infrastructure.EfRepositories;
using TextRpg.Infrastructure.JsonRepositories;

namespace TextRpg.Infrastructure;

/// <summary>
///   Extension methods for configuring infrastructure services.
/// </summary>
public static class ServiceCollectionExtensions
{
  #region Methods

  /// <summary>
  ///   Registers EF Core context, repositories, and JSON-based services in the DI container.
  /// </summary>
  /// <param name="services">The service collection to configure.</param>
  /// <param name="databasePath">Relative path to the SQLite database file.</param>
  public static void AddInfrastructure(this IServiceCollection services, string databasePath)
  {
    var connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databasePath)};";
    services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connectionString));
    services.AddScoped<ITraitRepository, TraitRepository>();
    services.AddScoped<IGreetingRepository, GreetingRepository>();
    services.AddScoped<ILocationRepository, LocationRepository>();
    services.AddScoped<ILocationOpeningHoursRepository, LocationOpeningHoursRepository>();
    services.AddScoped<IRoomRepository, RoomRepository>();
    services.AddScoped<IMovementRepository, MovementRepository>();
    services.AddScoped<IMovementNarrationRepository, MovementNarrationRepository>();
    services.AddScoped<INarrationRepository, NarrationRepository>();
    services.AddScoped<IExplorationActionRepository, ExplorationActionRepository>();
    services.AddScoped<IExplorationActionResultRepository, ExplorationActionResultRepository>();
    services.AddScoped<IExplorationActionResultNarrationRepository, ExplorationActionResultNarrationRepository>();
    services.AddScoped<IGameSaveRepository, GameSaveJsonRepository>();

    var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
    services.AddScoped<INameRepository>(_ => new NameJsonRepository(dataDir));
  }

  #endregion
}
