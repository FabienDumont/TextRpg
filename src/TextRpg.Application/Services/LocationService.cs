using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for managing locations.
/// </summary>
public class LocationService(ILocationRepository locationRepository) : ILocationService
{
  #region Implementation of ILocationService

  /// <inheritdoc />
  public async Task<Location> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await locationRepository.GetByIdAsync(id, cancellationToken);
  }

  /// <inheritdoc />
  public async Task<Location> GetPlayerSpawnAsync(CancellationToken cancellationToken)
  {
    return await locationRepository.GetPlayerSpawnAsync(cancellationToken);
  }

  #endregion
}
