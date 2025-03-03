using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for movements.
/// </summary>
public class MovementService(IMovementRepository movementRepository) : IMovementService
{
  #region Implementation of MovementService

  /// <inheritdoc />
  public async Task<List<Movement>> GetAvailableMovementsAsync(
    Guid currentLocationId, Guid? currentRoomId, CancellationToken cancellationToken
  )
  {
    return await movementRepository.GetAvailableMovementsAsync(currentLocationId, currentRoomId, cancellationToken);
  }

  #endregion
}
