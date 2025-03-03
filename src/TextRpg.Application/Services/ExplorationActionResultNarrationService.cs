using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service for exploration action result narrations.
/// </summary>
public class ExplorationActionResultNarrationService(
  IExplorationActionResultNarrationRepository explorationActionResultNarrationRepository
) : IExplorationActionResultNarrationService
{
  #region Implementation of IExplorationActionResultNarrationService

  public async Task<ExplorationActionResultNarration> GetExplorationActionResultNarrationAsync(
    Guid explorationActionResultId, Character character, CancellationToken cancellationToken
  )
  {
    var explorationActionResultNarration =
      await explorationActionResultNarrationRepository.GetByExplorationActionResultIdAsync(
        explorationActionResultId, character, cancellationToken
      );

    return explorationActionResultNarration;
  }

  #endregion
}
