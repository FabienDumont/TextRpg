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
  public async Task<Greeting?> GetAsync(Character character, CancellationToken cancellationToken)
  {
    var allGreetings = await Context.Greetings.ToListAsync(cancellationToken);

    var allConditions = await Context.Conditions.Where(c => c.ContextType == "Greeting").ToListAsync(cancellationToken);

    foreach (var greeting in allGreetings)
    {
      var conditions = allConditions.Where(c => c.ContextId == greeting.Id);

      var allSatisfied = conditions.All(condition => ConditionEvaluator.EvaluateCondition(condition, character));

      if (allSatisfied) return greeting.ToDomain();
    }

    return null;
  }

  #endregion
}
