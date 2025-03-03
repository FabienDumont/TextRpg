using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for exploration action results.
/// </summary>
public class ExplorationActionResultRepository(ApplicationContext context)
  : RepositoryBase(context), IExplorationActionResultRepository
{
  #region Implementation of IExplorationActionResultRepository

  /// <inheritdoc />
  public async Task<ExplorationActionResult> GetByExplorationActionIdAsync(
    Guid explorationActionId, Character character, CancellationToken cancellationToken
  )
  {
    var dataModel = await Context.ExplorationActionResults.FirstOrDefaultAsync(
      ear => ear.ExplorationActionId == explorationActionId &&
             (ear.MinEnergy == null || character.Energy >= ear.MinEnergy) &&
             (ear.MaxEnergy == null || character.Energy < ear.MaxEnergy) &&
             (ear.MinMoney == null || character.Money >= ear.MinMoney) &&
             (ear.MaxMoney == null || character.Money < ear.MaxMoney), cancellationToken
    ).ConfigureAwait(false);

    if (dataModel is null)
    {
      throw new InvalidOperationException(
        $"No appropriate exploration action result found for exploration action {explorationActionId}."
      );
    }

    return dataModel.ToDomain();
  }

  #endregion
}
