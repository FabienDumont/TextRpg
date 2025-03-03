namespace TextRpg.Application.Services;

/// <summary>
///   Service interface for managing narrations.
/// </summary>
public interface INarrationService
{
  #region Methods

  /// <summary>
  ///   Retrieves the narration text associated with a specific key.
  /// </summary>
  /// <param name="key">The key.</param>
  /// <param name="cancellationToken">A cancellation token.</param>
  /// <returns>The narration text.</returns>
  Task<string> GetNarrationTextByKeyAsync(string key, CancellationToken cancellationToken);

  #endregion
}
