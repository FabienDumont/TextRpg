using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
///   Repository interface for locations.
/// </summary>
public interface ILocationRepository
{
  #region Methods

  /// <summary>
  ///   Retrieves a location by its identifier.
  /// </summary>
  Task<Location> GetByIdAsync(Guid id, CancellationToken cancellationToken);

  /// <summary>
  ///   Retrieves the designated player spawn location.
  /// </summary>
  Task<Location> GetPlayerSpawnAsync(CancellationToken cancellationToken);

  #endregion
}
