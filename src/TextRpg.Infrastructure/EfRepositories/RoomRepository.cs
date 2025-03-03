using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for rooms.
/// </summary>
public class RoomRepository(ApplicationContext context) : RepositoryBase(context), IRoomRepository
{
  #region Implementation of IRoomRepository

  /// <inheritdoc />
  public async Task<Room> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    var dataModel = await Context.Rooms.FindAsync([id], cancellationToken).ConfigureAwait(false);

    if (dataModel is null)
    {
      throw new InvalidOperationException($"Room with ID {id} was not found.");
    }

    return dataModel.ToDomain();
  }

  /// <inheritdoc />
  public async Task<Room?> GetLocationEntryPointAsync(Guid locationId, CancellationToken cancellationToken)
  {
    var rooms = await Context.Rooms.ToListAsync(cancellationToken).ConfigureAwait(false);
    var spawn = rooms.FirstOrDefault(r => r.LocationId == locationId && r.IsEntryPoint);

    return spawn?.ToDomain();
  }

  #endregion
}
