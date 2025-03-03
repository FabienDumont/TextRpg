using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for managing worlds.
/// </summary>
public class WorldService(
  ICharacterService characterService, ILocationService locationService, IRoomService roomService
) : IWorldService
{
  #region Implementation of IWorldService

  /// <inheritdoc />
  public async Task<World> CreateNewWorldAsync(
    DateTime date, Character playerCharacter, GameSettings gameSettings, CancellationToken cancellationToken
  )
  {
    var spawnRoom = await roomService.GetPlayerSpawnAsync(cancellationToken);

    if (spawnRoom is null)
    {
      throw new InvalidOperationException("No spawn room found.");
    }

    var spawnLocation = await locationService.GetByIdAsync(spawnRoom.LocationId, cancellationToken);
    playerCharacter.MoveTo(spawnLocation.Id, spawnRoom.Id);
    var world = World.Create(date, [playerCharacter]);

    for (var i = 0; i < gameSettings.RandomNpcCount; i++)
    {
      var character = await characterService.CreateRandomCharacterAsync();
      world.AddCharacter(character);
    }

    return world;
  }

  /// <inheritdoc />
  public void AdvanceTime(World world, int minutes)
  {
    world.AdvanceTime(minutes);
  }

  #endregion
}
