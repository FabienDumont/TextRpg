using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TextRpg.Infrastructure.EfDataModels;

[Table("Traits")]
[PrimaryKey(nameof(Id))]
public class TraitDataModel
{
  [Column("Id", Order = 1)]
  [Required]
  public Guid Id { get; set; }

  [Column("Name", Order = 2)]
  [Required]
  [MaxLength(100)]
  public required string Name { get; set; }
}
