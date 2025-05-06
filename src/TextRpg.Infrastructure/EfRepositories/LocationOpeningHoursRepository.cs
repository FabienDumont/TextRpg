using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for locations' opening hours.
/// </summary>
public class LocationOpeningHoursRepository : ILocationOpeningHoursRepository
{
  #region Implementation of ILocationOpeningHoursRepository

  public async Task<List<LocationOpeningHours>> GetByLocationIdAsync(Guid locationId, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  #endregion
}
