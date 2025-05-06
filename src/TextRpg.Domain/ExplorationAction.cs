namespace TextRpg.Domain;

/// <summary>
///   Domain class representing an exploration action.
/// </summary>
public class ExplorationAction
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; }

  /// <summary>
  ///   Identifier of the location where the action can be done.
  /// </summary>
  public Guid LocationId { get; }

  /// <summary>
  ///   The action's label.
  /// </summary>
  public string Label { get; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private ExplorationAction(Guid id, Guid locationId, string label)
  {
    Id = id;
    LocationId = locationId;
    Label = label;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static ExplorationAction Create(Guid locationId, string label)
  {
    return new ExplorationAction(Guid.NewGuid(), locationId, label);
  }

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static ExplorationAction Load(Guid id, Guid locationId, string label)
  {
    return new ExplorationAction(id, locationId, label);
  }

  #endregion
}
