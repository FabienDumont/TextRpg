using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Helper;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for greetings.
/// </summary>
public class GreetingRepository(ApplicationContext context) : RepositoryBase(context), IGreetingRepository
{
  #region Implementation of IGreetingRepository

  /// <inheritdoc />
  public async Task<Greeting?> GetByRelationshipLevelAsync(
    int relationshipLevel, IEnumerable<Trait> traits, CancellationToken cancellationToken
  )
  {
    // Load all greetings from DB
    var allGreetings = await Context.Greetings.ToListAsync(cancellationToken);

    // Load all "Greeting" conditions
    var allConditions = await Context.Conditions.Where(c => c.ContextType == "Greeting").ToListAsync(cancellationToken);

    // Evaluate each greeting by attaching its conditions and testing them
    foreach (var greeting in allGreetings)
    {
      var conditions = allConditions.Where(c => c.ContextId == greeting.Id);

      var allSatisfied = conditions.All(condition =>
        ConditionEvaluator.EvaluateCondition(condition, relationshipLevel, traits)
      );

      if (allSatisfied) return greeting.ToDomain();
    }

    return null;
  }

  #endregion
}
