using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for managing locations.
/// </summary>
public class LocationService(
  ILocationRepository locationRepository, ILocationOpeningHoursService locationOpeningHoursService
) : ILocationService
{
  #region Implementation of ILocationService

  /// <inheritdoc />
  public async Task<Location> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await locationRepository.GetByIdAsync(id, cancellationToken);
  }

  public async Task<bool> IsLocationOpenAsync(
    Guid locationId, DayOfWeek day, TimeSpan time, CancellationToken cancellationToken
  )
  {
    var location = await GetByIdAsync(locationId, cancellationToken);

    if (location.IsAlwaysOpen)
    {
      return true;
    }

    return await locationOpeningHoursService.IsLocationOpenAsync(locationId, day, time, cancellationToken);
  }

  #endregion
}
