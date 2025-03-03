using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for exploration action results.
/// </summary>
public class ExplorationActionResultNarrationRepository(ApplicationContext context)
  : RepositoryBase(context), IExplorationActionResultNarrationRepository
{
  #region Implementation of IExplorationActionResultNarrationRepository

  /// <inheritdoc />
  public async Task<ExplorationActionResultNarration> GetByExplorationActionResultIdAsync(
    Guid explorationActionResultId, Character character, CancellationToken cancellationToken
  )
  {
    var dataModel = await Context.ExplorationActionResultNarrations.FirstOrDefaultAsync(
      ear => ear.ExplorationActionResultId == explorationActionResultId &&
             (ear.MinEnergy == null || character.Energy >= ear.MinEnergy) &&
             (ear.MaxEnergy == null || character.Energy < ear.MaxEnergy), cancellationToken
    ).ConfigureAwait(false);

    if (dataModel is null)
    {
      throw new InvalidOperationException(
        $"No appropriate exploration action result narration found for exploration action result {explorationActionResultId}."
      );
    }

    return dataModel.ToDomain();
  }

  #endregion
}
