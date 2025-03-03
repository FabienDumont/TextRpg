using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

/// <summary>
///   Repository interface for exploration action result narrations.
/// </summary>
public interface IExplorationActionResultNarrationRepository
{
  #region Methods

  /// <summary>
  ///   Retrieves exploration action result narration for given exploration action result identifier and character.
  /// </summary>
  Task<ExplorationActionResultNarration> GetByExplorationActionResultIdAsync(
    Guid explorationActionResultId, Character character, CancellationToken cancellationToken
  );

  #endregion
}
