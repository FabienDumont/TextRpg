﻿using Microsoft.EntityFrameworkCore;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Seeders;

/// <summary>
///   Exploration action data seeder.
/// </summary>
public class ExplorationActionSeeder : IDataSeeder
{
  #region Implementation of IDataSeeder

  /// <inheritdoc />
  public async Task SeedAsync(ApplicationContext context)
  {
    var home = await context.Locations.FirstAsync(l => l.Name == "Home");
    var bedroom = await context.Rooms.FirstAsync(r => r.Name == "Bedroom");

    var sleepAction = new ExplorationActionDataModel
    {
      Id = Guid.NewGuid(),
      LocationId = home.Id,
      RoomId = bedroom.Id,
      Label = "Sleep (10 hours)",
      NeededMinutes = 480
    };

    var napAction = new ExplorationActionDataModel
    {
      Id = Guid.NewGuid(),
      LocationId = home.Id,
      RoomId = bedroom.Id,
      Label = "Nap (1 hour)",
      NeededMinutes = 60
    };

    await context.ExplorationActions.AddRangeAsync(sleepAction, napAction);
    await context.SaveChangesAsync().ConfigureAwait(false);

    var sleepResultId = Guid.NewGuid();

    await context.ExplorationActionResults.AddAsync(
      new ExplorationActionResultDataModel
      {
        Id = sleepResultId,
        ExplorationActionId = sleepAction.Id,
        AddMinutes = true,
        EnergyChange = 100
      }
    );

    await context.ExplorationActionResultNarrations.AddRangeAsync(
      new ExplorationActionResultNarrationDataModel
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = sleepResultId,
        MinEnergy = null,
        MaxEnergy = 25,
        Text = "You collapse into bed, too tired to even pull the sheets over yourself."
      }, new ExplorationActionResultNarrationDataModel
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = sleepResultId,
        MinEnergy = 25,
        MaxEnergy = 50,
        Text = "You ease into bed, your body grateful for the rest."
      }, new ExplorationActionResultNarrationDataModel
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = sleepResultId,
        MinEnergy = 50,
        MaxEnergy = null,
        Text = "You're not very tired, but you lay down anyway, hoping to fall asleep."
      }
    );

    var napResultId = Guid.NewGuid();

    await context.ExplorationActionResults.AddAsync(
      new ExplorationActionResultDataModel
      {
        Id = napResultId,
        ExplorationActionId = napAction.Id,
        AddMinutes = true,
        EnergyChange = 10
      }
    );

    await context.ExplorationActionResultNarrations.AddRangeAsync(
      new ExplorationActionResultNarrationDataModel
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = napResultId,
        MinEnergy = null,
        MaxEnergy = 25,
        Text = "You sink into the mattress and quickly doze off, too drained to think."
      }, new ExplorationActionResultNarrationDataModel
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = napResultId,
        MinEnergy = 25,
        MaxEnergy = 50,
        Text = "You rest your head and fall asleep faster than expected. It's brief but helpful."
      }, new ExplorationActionResultNarrationDataModel
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = napResultId,
        MinEnergy = 50,
        MaxEnergy = null,
        Text = "You lie back and close your eyes, but your thoughts keep drifting. You barely nap at all."
      }
    );

    await context.SaveChangesAsync().ConfigureAwait(false);
  }

  #endregion
}
