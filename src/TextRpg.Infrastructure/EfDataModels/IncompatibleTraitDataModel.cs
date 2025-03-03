using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TextRpg.Infrastructure.EfDataModels;

[Table("IncompatibleTraits")]
[PrimaryKey(nameof(TraitId), nameof(IncompatibleTraitId))]
public class IncompatibleTraitDataModel
{
  [Column("TraitId", Order = 1)]
  [Required]
  [ForeignKey(nameof(TraitDataModel))]
  public Guid TraitId { get; set; }

  [Column("IncompatibleTraitId", Order = 2)]
  [Required]
  [ForeignKey(nameof(TraitDataModel))]
  public Guid IncompatibleTraitId { get; set; }
}
