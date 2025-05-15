﻿using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for exploration actions.
/// </summary>
public class ExplorationActionService(
  IExplorationActionRepository explorationActionRepository, ILocationService locationService,
  IExplorationActionResultService explorationActionResultService,
  IExplorationActionResultNarrationService explorationActionResultNarrationService
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

  public async Task<ExplorationActionResultNarration> ExecuteAsync(
    ExplorationAction action, GameSave save, CancellationToken cancellationToken
  )
  {
    var character = save.PlayerCharacter;

    // Get action result and narration
    var result = await explorationActionResultService.GetExplorationActionResultAsync(
      action.Id, character, cancellationToken
    );

    var narration = await explorationActionResultNarrationService.GetExplorationActionResultNarrationAsync(
      result.Id, character, cancellationToken
    );

    // Apply effects
    if (result.AddMinutes)
    {
      save.World.AdvanceTime(action.NeededMinutes);
    }

    if (result.EnergyChange is not null)
    {
      character.Energy += result.EnergyChange.Value;
    }

    if (result.MoneyChange is not null)
    {
      character.Money += result.MoneyChange.Value;
    }

    return narration;
  }

  #endregion
}
