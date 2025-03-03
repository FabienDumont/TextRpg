using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
/// Repository interface for rooms.
/// </summary>
public interface IRoomRepository
{
  #region Methods

  /// <summary>
  ///   Retrieves a room by its identifier.
  /// </summary>
  Task<Room> GetByIdAsync(Guid id, CancellationToken cancellationToken);

  /// <summary>
  ///   Retrieves the entry point room for a given location, if any.
  /// </summary>
  Task<Room?> GetLocationEntryPointAsync(Guid locationId, CancellationToken cancellationToken);

  #endregion
}
