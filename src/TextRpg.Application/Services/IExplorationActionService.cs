using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service interface for exploration actions.
/// </summary>
public interface IExplorationActionService
{
  #region Methods

  /// <summary>
  ///   Retrieves a list of available exploration actions based on the current location, room and time.
  /// </summary>
  Task<List<ExplorationAction>> GetExplorationActionsAsync(
    Guid locationId, Guid? roomId, DayOfWeek currentDay, TimeSpan currentTime, CancellationToken cancellationToken
  );

  #endregion
}
