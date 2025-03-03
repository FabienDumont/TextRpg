using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TextRpg.Infrastructure.EfDataModels;

[Table("Greetings")]
[PrimaryKey(nameof(Id))]
public class GreetingDataModel
{
  [Column("Id", Order = 1)]
  [Required]
  public Guid Id { get; set; }

  [Column("MinRelationship", Order = 2)]
  public int? MinRelationship { get; set; }

  [Column("MaxRelationship", Order = 3)]
  public int? MaxRelationship { get; set; }

  [Column("HasTrait", Order = 4)]
  [ForeignKey(nameof(TraitDataModel))]
  public Guid? HasTrait { get; set; }

  [Column("SpokenText", Order = 5)]
  [Required]
  [MaxLength(1000)]
  public required string SpokenText { get; set; }
}
