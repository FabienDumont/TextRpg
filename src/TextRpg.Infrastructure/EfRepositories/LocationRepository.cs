using Microsoft.EntityFrameworkCore;
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

  /// <inheritdoc />
  public async Task<Location> GetPlayerSpawnAsync(CancellationToken cancellationToken)
  {
    var spawn = await Context.Locations.FirstOrDefaultAsync(l => l.IsPlayerSpawn, cancellationToken)
      .ConfigureAwait(false);

    if (spawn is null)
    {
      throw new InvalidOperationException("Spawn location not found.");
    }

    return spawn.ToDomain();
  }

  #endregion
}
