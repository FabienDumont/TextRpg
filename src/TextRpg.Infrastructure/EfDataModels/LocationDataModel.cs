using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TextRpg.Infrastructure.EfDataModels;

/// <summary>
///   EF Core data model representing a location in the game world.
/// </summary>
[Table("Locations")]
[PrimaryKey(nameof(Id))]
public class LocationDataModel
{
  #region Properties

  /// <summary>
  ///   Unique identifier.
  /// </summary>
  [Column("Id", Order = 1)]
  [Required]
  public Guid Id { get; set; }

  /// <summary>
  ///   Name of the location.
  /// </summary>
  [Column("Name", Order = 2)]
  [Required]
  [MaxLength(100)]
  public required string Name { get; set; }

  /// <summary>
  ///   Flag is the location always open.
  /// </summary>
  [Column("IsAlwaysOpen", Order = 3)]
  [Required]
  public required bool IsAlwaysOpen { get; set; }

  #endregion
}
