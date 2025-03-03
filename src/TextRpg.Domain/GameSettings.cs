namespace TextRpg.Domain;

/// <summary>
///   Domain class representing the game's configuration settings.
/// </summary>
public class GameSettings
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; }

  /// <summary>
  ///   Number of random NPCs to generate when starting a new game.
  /// </summary>
  public int RandomNpcCount { get; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private GameSettings(Guid id, int randomNpcCount)
  {
    Id = id;
    RandomNpcCount = randomNpcCount;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static GameSettings Load(Guid id, int randomNpcCount)
  {
    return new GameSettings(id, randomNpcCount);
  }

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static GameSettings Create(int randomNpcCount)
  {
    return new GameSettings(Guid.NewGuid(), randomNpcCount);
  }

  #endregion
}
