using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for exploration actions.
/// </summary>
public class ExplorationActionService(
  IExplorationActionRepository explorationActionRepository, ILocationService locationService
) : IExplorationActionService
{
  #region Methods

  private static DayOfWeek GetNextDay(DayOfWeek day)
  {
    return day == DayOfWeek.Sunday ? DayOfWeek.Monday : (DayOfWeek) (((int) day + 1) % 7);
  }

  #endregion

  #region Implementation of IExplorationActionService

  /// <inheritdoc />
  public async Task<List<ExplorationAction>> GetExplorationActionsAsync(
    Guid locationId, Guid? roomId, DayOfWeek currentDay, TimeSpan currentTime, CancellationToken cancellationToken
  )
  {
    var location = await locationService.GetByIdAsync(locationId, cancellationToken);

    var explorationActions = await explorationActionRepository.GetByLocationAndRoomIdAsync(
      locationId, roomId, cancellationToken
    );

    if (location.IsAlwaysOpen)
    {
      return explorationActions;
    }

    var filtered = new List<ExplorationAction>();

    foreach (var action in explorationActions)
    {
      var timeAfterAction = currentTime.Add(TimeSpan.FromMinutes(action.NeededMinutes));

      var stillOpen = await locationService.IsLocationOpenAsync(
        locationId, timeAfterAction.Hours < currentTime.Hours ? GetNextDay(currentDay) : currentDay, timeAfterAction,
        cancellationToken
      );

      if (stillOpen)
      {
        filtered.Add(action);
      }
    }

    return filtered;
  }

  #endregion
}
