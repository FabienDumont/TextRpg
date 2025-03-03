using TextRpg.Domain;

namespace TextRpg.Infrastructure.JsonDataModels;

/// <summary>
///   JSON data model representing a character for serialization.
/// </summary>
public class CharacterDataModel
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; init; }

  /// <summary>
  ///   Name of the character.
  /// </summary>
  public string Name { get; init; } = string.Empty;

  /// <summary>
  ///   Age of the character.
  /// </summary>
  public int Age { get; init; }

  /// <summary>
  ///   Biological sex of the character.
  /// </summary>
  public BiologicalSex BiologicalSex { get; init; }

  /// <summary>
  ///   Identifiers of traits assigned to the character.
  /// </summary>
  public List<Guid> TraitsId { get; init; } = [];

  /// <summary>
  ///   Identifier of the current location (optional).
  /// </summary>
  public Guid? LocationId { get; set; }

  /// <summary>
  ///   Identifier of the current room (optional).
  /// </summary>
  public Guid? RoomId { get; set; }

  /// <summary>
  ///   Energy of the character.
  /// </summary>
  public int Energy { get; init; }

  /// <summary>
  ///   Money of the character.
  /// </summary>
  public int Money { get; init; }

  #endregion
}
