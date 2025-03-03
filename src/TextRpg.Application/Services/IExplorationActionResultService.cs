using TextRpg.Domain;

namespace TextRpg.Application.Services;

/// <summary>
///   Service interface for exploration action results.
/// </summary>
public interface IExplorationActionResultService
{
  #region Methods

  /// <summary>
  ///   Retrieves an exploration action result based on the exploration action identifier and the character.
  /// </summary>
  Task<ExplorationActionResult> GetExplorationActionResultAsync(
    Guid explorationActionId, Character character, CancellationToken cancellationToken
  );

  #endregion
}
