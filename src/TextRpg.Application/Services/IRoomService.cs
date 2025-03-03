using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service interface for managing rooms.
/// </summary>
public interface IRoomService
{
  #region Methods

  /// <summary>
  ///   Retrieves a room by its identifier.
  /// </summary>
  /// <param name="id">The unique identifier of the room.</param>
  /// <param name="cancellationToken">A cancellation token.</param>
  /// <returns>The room matching the specified identifier.</returns>
  Task<Room> GetByIdAsync(Guid id, CancellationToken cancellationToken);

  /// <summary>
  ///   Retrieves the default entry point room for a given location.
  /// </summary>
  /// <param name="locationId">The identifier of the location.</param>
  /// <param name="cancellationToken">A cancellation token.</param>
  /// <returns>The entry point room of the location, or null if none is found.</returns>
  Task<Room?> GetLocationEntryPointAsync(Guid locationId, CancellationToken cancellationToken);

  #endregion
}
