namespace TextRpg.Domain;

/// <summary>
/// Domain class representing a saved game state.
/// </summary>
public class GameSave
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; }

  /// <summary>
  ///   Name of the save file.
  /// </summary>
  public string Name { get; }

  /// <summary>
  ///   World state at the time of the save.
  /// </summary>
  public World World { get; }

  /// <summary>
  ///   Identifier of the player character in the world.
  /// </summary>
  public Guid PlayerCharacterId { get; }

  /// <summary>
  ///   The player character referenced by the save.
  /// </summary>
  public Character PlayerCharacter => World.Characters.First(c => c.Id == PlayerCharacterId);

  /// <summary>
  ///   Timestamp of when the save was created.
  /// </summary>
  public DateTime SavedAt { get; } = DateTime.UtcNow;

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private GameSave(Guid id, string name, Guid playerCharacterId, World world)
  {
    Id = id;
    Name = name;
    PlayerCharacterId = playerCharacterId;
    World = world;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static GameSave Load(Guid id, string name, Guid playerCharacterId, World world)
  {
    if (world.Characters.All(c => c.Id != playerCharacterId))
      throw new InvalidOperationException("Player character not found in character list.");

    return new GameSave(id, name, playerCharacterId, world);
  }

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static GameSave Create(string name, Character playerCharacter, World world)
  {
    ArgumentNullException.ThrowIfNull(playerCharacter);

    return new GameSave(Guid.NewGuid(), name, playerCharacter.Id, world);
  }

  #endregion
}
