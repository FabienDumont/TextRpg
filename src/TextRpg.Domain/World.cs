namespace TextRpg.Domain;

/// <summary>
///   Domain class representing the game world state, including time and characters.
/// </summary>
public class World
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; }

  /// <summary>
  ///   Current in-game date and time.
  /// </summary>
  public DateTime CurrentDate { get; private set; }

  /// <summary>
  ///   List of characters currently present in the world.
  /// </summary>
  public List<Character> Characters { get; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private World(Guid id, DateTime currentDate, List<Character> characters)
  {
    Id = id;
    CurrentDate = currentDate;
    Characters = characters ?? throw new ArgumentNullException(nameof(characters));
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static World Create(DateTime currentDate, List<Character> characters)
  {
    return new World(Guid.NewGuid(), currentDate, characters);
  }

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static World Load(Guid id, DateTime currentDate, List<Character> characters)
  {
    return new World(id, currentDate, characters);
  }

  /// <summary>
  ///   Adds a character to the world.
  /// </summary>
  public void AddCharacter(Character character)
  {
    Characters.Add(character);
  }

  /// <summary>
  ///   Advances the current world time by a number of minutes.
  /// </summary>
  public void AdvanceTime(int minutes)
  {
    CurrentDate = CurrentDate.AddMinutes(minutes);
  }

  #endregion
}
