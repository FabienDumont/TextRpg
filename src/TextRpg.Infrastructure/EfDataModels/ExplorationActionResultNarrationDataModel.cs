﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TextRpg.Infrastructure.EfDataModels;

[Table("ExplorationActionResultNarrations")]
[PrimaryKey(nameof(Id))]
public class ExplorationActionResultNarrationDataModel
{
  #region Properties

  [Column("Id")]
  [Required]
  public Guid Id { get; set; }

  [Column("ExplorationActionResultId")]
  [Required]
  public Guid ExplorationActionResultId { get; set; }

  [Column("MinEnergy")]
  public int? MinEnergy { get; set; }

  [Column("MaxEnergy")]
  public int? MaxEnergy { get; set; }

  [Column("Text")]
  [Required]
  [MaxLength(500)]
  public required string Text { get; set; }

  [ForeignKey(nameof(ExplorationActionResultId))]
  public ExplorationActionResultDataModel? ExplorationActionResult { get; set; }

  #endregion
}
