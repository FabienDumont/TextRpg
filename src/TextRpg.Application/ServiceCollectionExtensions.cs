using Microsoft.Extensions.DependencyInjection;
using TextRpg.Application.Services;

namespace TextRpg.Application;

/// <summary>
///   Extension methods for configuring application layer services.
/// </summary>
public static class ServiceCollectionExtensions
{
  #region Methods

  /// <summary>
  ///   Registers application services in the DI container.
  /// </summary>
  /// <param name="services">The service collection to configure.</param>
  public static void AddApplication(this IServiceCollection services)
  {
    services.AddScoped<ITraitService, TraitService>();
    services.AddScoped<ILocationService, LocationService>();
    services.AddScoped<ILocationOpeningHoursService, LocationOpeningHoursService>();
    services.AddScoped<IRoomService, RoomService>();
    services.AddScoped<ISaveService, SaveService>();
    services.AddScoped<IWorldService, WorldService>();
    services.AddScoped<ICharacterService, CharacterService>();
    services.AddScoped<IMovementService, MovementService>();
    services.AddScoped<IMovementNarrationService, MovementNarrationService>();
    services.AddScoped<INarrationService, NarrationService>();
    services.AddScoped<IExplorationActionService, ExplorationActionService>();
    services.AddScoped<IExplorationActionResultService, ExplorationActionResultService>();
    services.AddScoped<IExplorationActionResultNarrationService, ExplorationActionResultNarrationService>();
  }

  #endregion
}
