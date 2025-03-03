using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for exploration action results.
/// </summary>
public class ExplorationActionResultService(IExplorationActionResultRepository explorationActionResultRepository)
  : IExplorationActionResultService
{
  #region Implementation of IExplorationActionResultService

  /// <inheritdoc />
  public async Task<ExplorationActionResult> GetExplorationActionResultAsync(
    Guid explorationActionId, Character character, CancellationToken cancellationToken
  )
  {
    var explorationActionResult =
      await explorationActionResultRepository.GetByExplorationActionIdAsync(
        explorationActionId, character, cancellationToken
      );

    return explorationActionResult;
  }

  #endregion
}
