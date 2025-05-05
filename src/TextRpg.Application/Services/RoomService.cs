using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for managing rooms.
/// </summary>
public class RoomService(IRoomRepository roomRepository) : IRoomService
{
  #region Implementation of IRoomService

  /// <inheritdoc />
  public async Task<Room> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await roomRepository.GetByIdAsync(id, cancellationToken);
  }

  /// <inheritdoc />
  public async Task<Room?> GetPlayerSpawnAsync(CancellationToken cancellationToken)
  {
    return await roomRepository.GetPlayerSpawnAsync(cancellationToken);
  }

  #endregion
}
