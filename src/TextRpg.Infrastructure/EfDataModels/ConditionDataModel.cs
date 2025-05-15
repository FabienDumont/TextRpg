using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TextRpg.Infrastructure.EfDataModels;

/// <summary>
///   EF Core data model representing a condition.
/// </summary>
[Table("Conditions")]
[PrimaryKey(nameof(Id))]
public class ConditionDataModel
{
  [Required]
  public Guid Id { get; set; }

  [Required]
  [MaxLength(100)]
  public required string ContextType { get; set; }

  [Required]
  public Guid ContextId { get; set; }

  [Required]
  [MaxLength(100)]
  public required string ConditionType { get; set; }

  [MaxLength(10)]
  public string? OperandLeft { get; set; }

  [Required]
  [MaxLength(100)]
  public required string Operator { get; set; }

  [MaxLength(10)]
  public string? OperandRight { get; set; }

  public bool Negate { get; set; }
}
