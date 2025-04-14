namespace TextRpg.Domain;

public class GameSave
{
  #region Properties

  public Guid Id { get; }
  public string Name { get; }
  public World World { get; }
  public Guid PlayerCharacterId { get; }
  public Character PlayerCharacter => World.Characters.First(c => c.Id == PlayerCharacterId);
  public DateTime SavedAt { get; } = DateTime.UtcNow;

  #endregion

  #region Ctors

  private GameSave(Guid id, string name, Guid playerCharacterId, World world)
  {
    Id = id;
    Name = name;
    PlayerCharacterId = playerCharacterId;
    World = world;
  }

  #endregion

  #region Methods

  public static GameSave Load(Guid id, string name, Guid playerCharacterId, World world)
  {
    if (world.Characters.All(c => c.Id != playerCharacterId))
      throw new InvalidOperationException("Player character not found in character list.");

    return new GameSave(id, name, playerCharacterId, world);
  }

  public static GameSave Create(string name, Character playerCharacter, World world)
  {
    ArgumentNullException.ThrowIfNull(playerCharacter);

    return new GameSave(Guid.NewGuid(), name, playerCharacter.Id, world);
  }

  #endregion
}
