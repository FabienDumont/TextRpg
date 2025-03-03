namespace TextRpg.Domain;

public class GameSave
{
  #region Properties

  public Guid Id { get; }
  public string Name { get; }
  public Guid PlayerCharacterId { get; }
  public IEnumerable<Character> Characters { get; }
  public Character PlayerCharacter => Characters.First(c => c.Id == PlayerCharacterId);
  public DateTime SavedAt { get; } = DateTime.UtcNow;

  #endregion

  #region Ctors

  private GameSave(Guid id, string name, Guid playerCharacterId, List<Character> characters)
  {
    Id = id;
    Name = name;
    PlayerCharacterId = playerCharacterId;
    Characters = characters ?? throw new ArgumentNullException(nameof(characters));
  }

  #endregion

  #region Methods

  public static GameSave Load(Guid id, string name, Guid playerCharacterId, List<Character> characters)
  {
    if (characters.All(c => c.Id != playerCharacterId))
      throw new InvalidOperationException("Player character not found in character list.");

    return new GameSave(id, name, playerCharacterId, characters);
  }

  public static GameSave Create(string name, Character playerCharacter)
  {
    ArgumentNullException.ThrowIfNull(playerCharacter);

    var characters = new List<Character> {playerCharacter};
    return new GameSave(Guid.NewGuid(), name, playerCharacter.Id, characters);
  }

  #endregion
}
