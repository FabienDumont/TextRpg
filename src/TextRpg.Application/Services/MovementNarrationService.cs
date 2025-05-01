using TextRpg.Application.Repositories;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for managing movement narrations.
/// </summary>
public class MovementNarrationService(IMovementNarrationRepository repository) : IMovementNarrationService
{
  #region Implementation of IMovementNarrationService

  /// <inheritdoc />
  public async Task<string> GetNarrationTextAsync(Guid movementId, CancellationToken cancellationToken)
  {
    var narration = await repository.GetMovementNarrationFromMovementIdAsync(movementId, cancellationToken);

    return narration.Text;
  }

  #endregion
}
