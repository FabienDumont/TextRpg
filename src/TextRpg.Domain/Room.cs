namespace TextRpg.Domain;

/// <summary>
/// Domain class representing a room inside a location.
/// </summary>
public class Room
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; set; }

  /// <summary>
  ///   Identifier of the location this room belongs to.
  /// </summary>
  public Guid LocationId { get; set; }

  /// <summary>
  ///   Name of the room.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  ///   Indicates whether this room is the default entry point for the location.
  /// </summary>
  public bool IsEntryPoint { get; set; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private Room(Guid id, Guid locationId, string name, bool isEntryPoint)
  {
    Id = id;
    LocationId = locationId;
    Name = name;
    IsEntryPoint = isEntryPoint;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static Room Create(Guid locationId, string name, bool isEntryPoint)
  {
    return new Room(Guid.NewGuid(), locationId, name, isEntryPoint);
  }

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static Room Load(Guid id, Guid locationId, string name, bool isEntryPoint)
  {
    return new Room(id, locationId, name, isEntryPoint);
  }

  #endregion
}
