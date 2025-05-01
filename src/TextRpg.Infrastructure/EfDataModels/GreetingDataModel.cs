using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TextRpg.Infrastructure.EfDataModels;

/// <summary>
///   EF Core data model representing a greeting entry.
/// </summary>
[Table("Greetings")]
[PrimaryKey(nameof(Id))]
public class GreetingDataModel
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  [Column("Id", Order = 1)]
  [Required]
  public Guid Id { get; set; }

  /// <summary>
  ///   Minimum relationship level required to trigger the greeting (inclusive).
  /// </summary>
  [Column("MinRelationship", Order = 2)]
  public int? MinRelationship { get; set; }

  /// <summary>
  ///   Maximum relationship level allowed to trigger the greeting (exclusive).
  /// </summary>
  [Column("MaxRelationship", Order = 3)]
  public int? MaxRelationship { get; set; }

  /// <summary>
  ///   Trait required for this greeting to be applicable (optional).
  /// </summary>
  [Column("HasTrait", Order = 4)]
  [ForeignKey(nameof(TraitDataModel))]
  public Guid? HasTrait { get; set; }

  /// <summary>
  ///   The spoken text of the greeting.
  /// </summary>
  [Column("SpokenText", Order = 5)]
  [Required]
  [MaxLength(1000)]
  public required string SpokenText { get; set; }

  #endregion
}
