using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
///   Repository interface for exploration action results.
/// </summary>
public interface IExplorationActionResultRepository
{
  #region Methods

  /// <summary>
  ///   Retrieves exploration action result for given exploration action identifier and character.
  /// </summary>
  Task<ExplorationActionResult> GetByExplorationActionIdAsync(
    Guid explorationActionId, Character character, CancellationToken cancellationToken
  );

  #endregion
}
