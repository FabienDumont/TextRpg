﻿using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for movements.
/// </summary>
public class MovementService(IMovementRepository movementRepository, ILocationService locationService)
  : IMovementService
{
  #region Implementation of IMovementService

  /// <inheritdoc />
  public async Task<List<Movement>> GetAvailableMovementsAsync(
    Guid currentLocationId, Guid? currentRoomId, DayOfWeek day, TimeSpan time, CancellationToken cancellationToken
  )
  {
    var movements = await movementRepository.GetMovementsAsync(currentLocationId, currentRoomId, cancellationToken);

    List<Movement> availableMovements = [];

    foreach (var movement in movements)
    {
      var locationDestinationId = movement.ToLocationId;

      if (await locationService.IsLocationOpenAsync(locationDestinationId, day, time, cancellationToken))
      {
        availableMovements.Add(movement);
      }
    }

    return availableMovements;
  }

  #endregion
}
