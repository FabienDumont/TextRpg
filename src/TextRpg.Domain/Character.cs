namespace TextRpg.Domain;

/// <summary>
///   Domain class representing a character in the game.
/// </summary>
public class Character
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; }

  /// <summary>
  ///   Name of the character.
  /// </summary>
  public string Name { get; }

  /// <summary>
  ///   Age of the character.
  /// </summary>
  public int Age { get; }

  /// <summary>
  ///   Biological sex of the character.
  /// </summary>
  public BiologicalSex BiologicalSex { get; }

  /// <summary>
  ///   List of trait identifiers assigned to the character.
  /// </summary>
  public List<Guid> TraitsId { get; } = [];

  /// <summary>
  ///   Identifier of the character's current location (optional).
  /// </summary>
  public Guid? LocationId { get; private set; }

  /// <summary>
  ///   Identifier of the character's current room (optional).
  /// </summary>
  public Guid? RoomId { get; private set; }

  /// <summary>
  ///   Energy of the character.
  /// </summary>
  public int Energy { get; set; } = 100;

  /// <summary>
  ///   Money of the character.
  /// </summary>
  public int Money { get; set; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private Character(Guid id, string name, int age, BiologicalSex biologicalSex)
  {
    Id = id;
    Name = name;
    Age = age;
    BiologicalSex = biologicalSex;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static Character Create(string name, int age, BiologicalSex biologicalSex)
  {
    return new Character(Guid.NewGuid(), name, age, biologicalSex);
  }

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static Character Load(Guid id, string name, int age, BiologicalSex biologicalSex)
  {
    return new Character(id, name, age, biologicalSex);
  }

  /// <summary>
  ///   Adds traits to the character.
  /// </summary>
  public void AddTraits(IEnumerable<Guid> traitIds)
  {
    TraitsId.AddRange(traitIds);
  }

  /// <summary>
  ///   Moves the character to a new location and room.
  /// </summary>
  public void MoveTo(Guid? locationId, Guid? roomId)
  {
    LocationId = locationId;
    RoomId = roomId;
  }

  #endregion
}

/// <summary>
///   Enumeration for biological sex.
/// </summary>
public enum BiologicalSex
{
  Male,
  Female
}
