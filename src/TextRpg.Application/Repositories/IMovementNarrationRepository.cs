﻿using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
///   Repository interface for movement narrations.
/// </summary>
public interface IMovementNarrationRepository
{
  #region Methods

  /// <summary>
  ///   Retrieves the narration associated with a specific movement.
  /// </summary>
  Task<MovementNarration> GetMovementNarrationFromMovementIdAsync(Guid movementId, CancellationToken cancellationToken);

  #endregion
}
