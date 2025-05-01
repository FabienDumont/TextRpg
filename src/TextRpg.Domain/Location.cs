namespace TextRpg.Domain;

/// <summary>
///   Domain class representing a location in the game world.
/// </summary>
public class Location
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; set; }

  /// <summary>
  ///   Name of the location.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  ///   Indicates whether this location is the player's spawn point.
  /// </summary>
  public bool IsPlayerSpawn { get; set; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private Location(Guid id, string name, bool isPlayerSpawn)
  {
    Id = id;
    Name = name;
    IsPlayerSpawn = isPlayerSpawn;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static Location Create(string name, bool isPlayerSpawn)
  {
    return new Location(Guid.NewGuid(), name, isPlayerSpawn);
  }

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static Location Load(Guid id, string name, bool isPlayerSpawn)
  {
    return new Location(id, name, isPlayerSpawn);
  }

  #endregion
}
