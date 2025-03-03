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
  ///   Minimum energy to get this result.
  /// </summary>
  public int? MinEnergy { get; }

  /// <summary>
  ///   Maximum energy to get this result.
  /// </summary>
  public int? MaxEnergy { get; }

  /// <summary>
  ///   Minimum money to get this result.
  /// </summary>
  public int? MinMoney { get; }

  /// <summary>
  ///   Maximum money to get this result.
  /// </summary>
  public int? MaxMoney { get; }

  /// <summary>
  ///   Flag should the required minutes of the exploration action be added.
  /// </summary>
  public bool AddMinutes { get; set; }

  /// <summary>
  ///   Change in energy after performing the action.
  /// </summary>
  public int? EnergyChange { get; }

  /// <summary>
  ///   Change in money after performing the action.
  /// </summary>
  public int? MoneyChange { get; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Private constructor used internally.
  /// </summary>
  private ExplorationActionResult(
    Guid id, Guid explorationActionId, int? minEnergy, int? maxEnergy, int? minMoney, int? maxMoney, bool addMinutes,
    int? energyChange, int? moneyChange
  )
  {
    Id = id;
    ExplorationActionId = explorationActionId;
    MinEnergy = minEnergy;
    MaxEnergy = maxEnergy;
    MinMoney = minMoney;
    MaxMoney = maxMoney;
    AddMinutes = addMinutes;
    EnergyChange = energyChange;
    MoneyChange = moneyChange;
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Factory method to create a new instance.
  /// </summary>
  public static ExplorationActionResult Create(
    Guid explorationActionId, int? minEnergy, int? maxEnergy, int? minMoney, int? maxMoney, bool addMinutes,
    int? energyChange, int? moneyChange
  )
  {
    return new ExplorationActionResult(
      Guid.NewGuid(), explorationActionId, minEnergy, maxEnergy, minMoney, maxMoney, addMinutes, energyChange,
      moneyChange
    );
  }

  /// <summary>
  ///   Factory method to load an existing instance from persistence.
  /// </summary>
  public static ExplorationActionResult Load(
    Guid id, Guid explorationActionId, int? minEnergy, int? maxEnergy, int? minMoney, int? maxMoney, bool addMinutes,
    int? energyChange, int? moneyChange
  )
  {
    return new ExplorationActionResult(
      id, explorationActionId, minEnergy, maxEnergy, minMoney, maxMoney, addMinutes, energyChange, moneyChange
    );
  }

  #endregion
}
