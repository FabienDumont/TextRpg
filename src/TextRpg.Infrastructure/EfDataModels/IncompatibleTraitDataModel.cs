using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TextRpg.Infrastructure.EfDataModels;

/// <summary>
///   EF Core data model representing a pair of incompatible traits.
/// </summary>
[Table("IncompatibleTraits")]
[PrimaryKey(nameof(TraitId), nameof(IncompatibleTraitId))]
public class IncompatibleTraitDataModel
{
  #region Properties

  /// <summary>
  ///   Identifier of the primary trait.
  /// </summary>
  [Column("TraitId", Order = 1)]
  [Required]
  [ForeignKey(nameof(TraitDataModel))]
  public Guid TraitId { get; set; }

  /// <summary>
  ///   Identifier of the trait that is incompatible with the primary trait.
  /// </summary>
  [Column("IncompatibleTraitId", Order = 2)]
  [Required]
  [ForeignKey(nameof(TraitDataModel))]
  public Guid IncompatibleTraitId { get; set; }

  #endregion
}
