namespace TextRpg.Domain;

/// <summary>
///   Domain class representing a greeting message tied to relationship level and traits.
/// </summary>
public class Greeting
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; }

  /// <summary>
  ///   Minimum relationship level required to trigger the greeting (inclusive).
  /// </summary>
  public int? MinRelationship { get; }

  /// <summary>
  ///   Maximum relationship level allowed to trigger the greeting (exclusive).
  /// </summary>
  public int? MaxRelationship { get; }

  /// <summary>
  ///   Trait required for this greeting to be used (optional).
  /// </summary>
  public Guid? HasTrait { get; }

  /// <summary>
  ///   Text spoken when the greeting is triggered.
  /// </summary>
  public string SpokenText { get; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private Greeting(Guid id, int? minRelationship, int? maxRelationship, Guid? hasTrait, string spokenText)
  {
    Id = id;
    MinRelationship = minRelationship;
    MaxRelationship = maxRelationship;
    HasTrait = hasTrait;
    SpokenText = spokenText;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static Greeting Create(int? minRelationship, int? maxRelationship, Guid? hasTrait, string spokenText)
  {
    return new Greeting(Guid.NewGuid(), minRelationship, maxRelationship, hasTrait, spokenText);
  }

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static Greeting Load(Guid id, int? minRelationship, int? maxRelationship, Guid? hasTrait, string spokenText)
  {
    return new Greeting(id, minRelationship, maxRelationship, hasTrait, spokenText);
  }

  #endregion
}
