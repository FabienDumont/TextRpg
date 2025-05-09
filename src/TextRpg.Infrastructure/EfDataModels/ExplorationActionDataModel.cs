using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextRpg.Infrastructure.EfDataModels;

/// <summary>
///   EF Core data model representing an exploration action.
/// </summary>
public class ExplorationActionDataModel
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  [Column("Id", Order = 1)]
  [Required]
  public Guid Id { get; set; }

  /// <summary>
  ///   Identifier of the location this entry belongs to.
  /// </summary>
  [Column("LocationId", Order = 2)]
  [Required]
  public required Guid LocationId { get; set; }

  /// <summary>
  ///   Identifier of the room this entry belongs to.
  /// </summary>
  [Column("RoomId", Order = 3)]
  public Guid? RoomId { get; set; }

  /// <summary>
  ///   Label of the exploration action.
  /// </summary>
  [Column("Text", Order = 4)]
  [Required]
  [MaxLength(500)]
  public required string Label { get; set; }

  /// <summary>
  ///   Needed minutes to do the action.
  /// </summary>
  [Column("NeededMinutes", Order = 5)]
  [Required]
  public required int NeededMinutes { get; set; }

  /// <summary>
  ///   Navigation property to the location.
  /// </summary>
  [ForeignKey(nameof(LocationId))]
  public LocationDataModel? Location { get; set; }

  #endregion
}
