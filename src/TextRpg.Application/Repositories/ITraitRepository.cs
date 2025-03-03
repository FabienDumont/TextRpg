using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
///   Repository interface for traits.
/// </summary>
public interface ITraitRepository
{
  #region Methods

  /// <summary>
  ///   Retrieves all available traits.
  /// </summary>
  Task<IReadOnlyCollection<Trait>> GetAllAsync(CancellationToken cancellationToken);

  /// <summary>
  ///   Retrieves traits compatible with the selected trait identifiers.
  /// </summary>
  Task<IReadOnlyCollection<Trait>> GetCompatibleTraitsAsync(
    IEnumerable<Guid> selectedTraitsIdsEnumerable, CancellationToken cancellationToken
  );

  #endregion
}
