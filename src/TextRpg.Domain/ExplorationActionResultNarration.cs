namespace TextRpg.Domain;

/// <summary>
///   Domain class representing an exploration action result narration.
/// </summary>
public class ExplorationActionResultNarration
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; }

  /// <summary>
  ///   Exploration action result identifier.
  /// </summary>
  public Guid ExplorationActionResultId { get; }

  /// <summary>
  ///   Minimum energy to get this narration.
  /// </summary>
  public int? MinEnergy { get; }

  /// <summary>
  ///   Maximum energy to get this narration.
  /// </summary>
  public int? MaxEnergy { get; }

  /// <summary>
  ///   Text of the narration.
  /// </summary>
  public string Text { get; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private ExplorationActionResultNarration(
    Guid id, Guid explorationActionResultId, int? minEnergy, int? maxEnergy, string text
  )
  {
    Id = id;
    ExplorationActionResultId = explorationActionResultId;
    MinEnergy = minEnergy;
    MaxEnergy = maxEnergy;
    Text = text;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static ExplorationActionResultNarration Create(
    Guid explorationActionId, int? minEnergy, int? maxEnergy, string text
  )
  {
    return new ExplorationActionResultNarration(Guid.NewGuid(), explorationActionId, minEnergy, maxEnergy, text);
  }

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static ExplorationActionResultNarration Load(
    Guid id, Guid explorationActionId, int? minEnergy, int? maxEnergy, string text
  )
  {
    return new ExplorationActionResultNarration(id, explorationActionId, minEnergy, maxEnergy, text);
  }

  #endregion
}
