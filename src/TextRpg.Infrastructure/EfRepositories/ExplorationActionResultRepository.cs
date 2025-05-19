using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Helper;
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
    var results = await Context.ExplorationActionResults.Where(n => n.ExplorationActionId == explorationActionId)
      .ToListAsync(cancellationToken);

    var narrationIds = results.Select(n => n.Id).ToList();
    var allConditions = await Context.Conditions
      .Where(c => c.ContextType == "ExplorationActionResult" && narrationIds.Contains(c.ContextId))
      .ToListAsync(cancellationToken);

    foreach (var explorationActionResult in results)
    {
      var conditions = allConditions.Where(c => c.ContextId == explorationActionResult.Id);

      var allSatisfied = conditions.All(condition => ConditionEvaluator.EvaluateCondition(condition, character));

      if (allSatisfied) return explorationActionResult.ToDomain();
    }

    throw new InvalidOperationException(
      $"No appropriate exploration action result found for exploration action {explorationActionId}."
    );
  }

  #endregion
}
