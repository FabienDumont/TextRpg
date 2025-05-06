namespace TextRpg.Domain;

/// <summary>
///   Domain class representing an exploration action result.
/// </summary>
public class ExplorationActionResult
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  public Guid Id { get; }

  /// <summary>
  ///   Exploration action identifier.
  /// </summary>
  public Guid ExplorationActionId { get; }

  /// <summary>
  ///   Minimum energy required to perform the action.
  /// </summary>
  public int MinEnergy { get; }

  /// <summary>
  ///   Minimum money required to perform the action.
  /// </summary>
  public int MinMoney { get; }

  /// <summary>
  ///   The action result's description.
  /// </summary>
  public string Description { get; }

  /// <summary>
  ///   Change in energy after performing the action.
  /// </summary>
  public int EnergyChange { get; }

  /// <summary>
  ///   Change in money after performing the action.
  /// </summary>
  public int MoneyChange { get; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private ExplorationActionResult(
    Guid id, Guid explorationActionId, int minEnergy, int minMoney, string description, int energyChange,
    int moneyChange
  )
  {
    Id = id;
    ExplorationActionId = explorationActionId;
    MinEnergy = minEnergy;
    MinMoney = minMoney;
    Description = description;
    EnergyChange = energyChange;
    MoneyChange = moneyChange;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static ExplorationActionResult Create(
    Guid explorationActionId, int minEnergy, int minMoney, string description, int energyChange, int moneyChange
  )
  {
    return new ExplorationActionResult(
      Guid.NewGuid(), explorationActionId, minEnergy, minMoney, description, energyChange, moneyChange
    );
  }

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static ExplorationActionResult Load(
    Guid id, Guid explorationActionId, int minEnergy, int minMoney, string description, int energyChange,
    int moneyChange
  )
  {
    return new ExplorationActionResult(
      id, explorationActionId, minEnergy, minMoney, description, energyChange, moneyChange
    );
  }

  #endregion
}
