using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TextRpg.Infrastructure.EfDataModels;

[Table("ExplorationActionResults")]
[PrimaryKey(nameof(Id))]
public class ExplorationActionResultDataModel
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  [Column("Id", Order = 1)]
  [Required]
  public Guid Id { get; set; }

  /// <summary>
  ///   Identifier of the exploration action this entry belongs to.
  /// </summary>
  [Column("ExplorationActionId", Order = 2)]
  [Required]
  public required Guid ExplorationActionId { get; set; }

  /// <summary>
  ///   Minimum energy needed to get this result.
  /// </summary>
  [Column("MinEnergy", Order = 3)]
  public int? MinEnergy { get; set; }

  /// <summary>
  ///   Maximum energy needed to get this result.
  /// </summary>
  [Column("MaxEnergy", Order = 4)]
  public int? MaxEnergy { get; set; }

  /// <summary>
  ///   Minimum money needed to get this result.
  /// </summary>
  public int? MinMoney { get; set; }

  /// <summary>
  ///   Maximum money needed to get this result.
  /// </summary>
  [Column("MaxMoney", Order = 6)]
  public int? MaxMoney { get; set; }

  /// <summary>
  ///   Label of the exploration action.
  /// </summary>
  [Column("AddMinutes", Order = 7)]
  [Required]
  public bool AddMinutes { get; set; }

  /// <summary>
  ///   Energy change when getting this result.
  /// </summary>
  [Column("EnergyChange", Order = 8)]
  public int? EnergyChange { get; set; }

  /// <summary>
  ///   Money change when getting this result.
  /// </summary>
  [Column("MoneyChange", Order = 9)]
  public int? MoneyChange { get; set; }

  /// <summary>
  ///   Navigation property to the location.
  /// </summary>
  [ForeignKey(nameof(ExplorationActionId))]
  public ExplorationActionDataModel? ExplorationAction { get; set; }

  #endregion
}
