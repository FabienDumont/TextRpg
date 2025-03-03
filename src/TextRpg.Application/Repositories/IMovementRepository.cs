using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
/// Repository interface for movements.
/// </summary>
public interface IMovementRepository
{
  /// <summary>
  ///   Retrieves all movements available from the specified location and room.
  /// </summary>
  Task<List<Movement>> GetAvailableMovementsAsync(
    Guid currentLocationId, Guid? currentRoomId, CancellationToken cancellationToken
  );
}
