using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for exploration actions.
/// </summary>
public class ExplorationActionRepository(ApplicationContext context)
  : RepositoryBase(context), IExplorationActionRepository
{
  #region Implementation of IExplorationActionRepository

  /// <inheritdoc />
  public async Task<List<ExplorationAction>> GetByLocationAndRoomIdAsync(
    Guid locationId, Guid? roomId, CancellationToken cancellationToken
  )
  {
    var explorationActions = await Context.ExplorationActions
      .Where(ea => ea.LocationId == locationId && ea.RoomId == roomId).ToListAsync(cancellationToken)
      .ConfigureAwait(false);

    return explorationActions.ToDomainCollection();
  }

  #endregion
}
