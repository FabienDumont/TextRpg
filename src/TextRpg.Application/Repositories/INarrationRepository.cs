using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
///   Repository interface for narrations.
/// </summary>
public interface INarrationRepository
{
  #region Methods

  /// <summary>
  ///   Retrieves the narration associated with a specific key.
  /// </summary>
  Task<Narration> GetNarrationByKeyAsync(string key, CancellationToken cancellationToken);

  #endregion
}
