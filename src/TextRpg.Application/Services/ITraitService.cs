using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service interface for managing traits.
/// </summary>
public interface ITraitService
{
  #region Methods

  /// <summary>
  ///   Retrieves all available traits.
  /// </summary>
  /// <param name="cancellationToken">A cancellation token.</param>
  /// <returns>A read-only collection of traits.</returns>
  Task<IReadOnlyCollection<Trait>> GetAllTraitsAsync(CancellationToken cancellationToken);

  /// <summary>
  ///   Retrieves traits compatible with a given set of selected trait identifiers.
  /// </summary>
  /// <param name="selectedTraitsIds">The identifiers of already selected traits.</param>
  /// <param name="cancellationToken">A cancellation token.</param>
  /// <returns>A read-only collection of traits compatible with the selected ones.</returns>
  Task<IReadOnlyCollection<Trait>> GetCompatibleTraitsAsync(
    IEnumerable<Guid> selectedTraitsIds, CancellationToken cancellationToken
  );

  #endregion
}
