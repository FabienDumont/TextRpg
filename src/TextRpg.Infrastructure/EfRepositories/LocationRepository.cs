using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for locations.
/// </summary>
public class LocationRepository(ApplicationContext context) : RepositoryBase(context), ILocationRepository
{
  #region Implementation of ILocationRepository

  /// <inheritdoc />
  public async Task<Location> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    var dataModel = await Context.Locations.FindAsync([id], cancellationToken).ConfigureAwait(false);

    if (dataModel is null)
    {
      throw new InvalidOperationException($"Location with ID {id} was not found.");
    }

    return dataModel.ToDomain();
  }

  #endregion
}
