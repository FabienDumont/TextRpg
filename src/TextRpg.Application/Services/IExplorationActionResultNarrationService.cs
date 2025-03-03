using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service interface for exploration action result narrations.
/// </summary>
public interface IExplorationActionResultNarrationService
{
  #region Methods

  /// <summary>
  ///   Retrieves an exploration action result narration based on the exploration action result identifier and the character.
  /// </summary>
  Task<ExplorationActionResultNarration> GetExplorationActionResultNarrationAsync(
    Guid explorationActionResultId, Character character, CancellationToken cancellationToken
  );

  #endregion
}
