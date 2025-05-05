using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service interface for managing locations.
/// </summary>
public interface ILocationService
{
  #region Methods

  /// <summary>
  ///   Retrieves a location by its unique identifier.
  /// </summary>
  /// <param name="id">The unique identifier of the location.</param>
  /// <param name="cancellationToken">A cancellation token.</param>
  /// <returns>The matching location.</returns>
  Task<Location> GetByIdAsync(Guid id, CancellationToken cancellationToken);

  #endregion
}
