using TextRpg.Application.Repositories;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for managing narrations.
/// </summary>
public class NarrationService(INarrationRepository repository) : INarrationService
{
  #region Implementation of INarrationService

  /// <inheritdoc />
  public async Task<string> GetNarrationTextByKeyAsync(string key, CancellationToken cancellationToken)
  {
    var narration = await repository.GetNarrationByKeyAsync(key, cancellationToken);

    return narration.Text;
  }

  #endregion
}
