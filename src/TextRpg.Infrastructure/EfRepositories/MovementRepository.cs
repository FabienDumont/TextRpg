using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for movements.
/// </summary>
public class MovementRepository(ApplicationContext context) : RepositoryBase(context), IMovementRepository
{
  #region Implementation of IMovementRepository

  /// <inheritdoc />
  public async Task<List<Movement>> GetMovementsAsync(
    Guid currentLocationId, Guid? currentRoomId, CancellationToken cancellationToken
  )
  {
    var movements = await Context.Movements
      .Where(m => m.FromLocationId == currentLocationId && m.FromRoomId == currentRoomId).ToListAsync(cancellationToken)
      .ConfigureAwait(false);

    return movements.ToDomainCollection();
  }

  #endregion
}
