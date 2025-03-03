using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
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
    var greetingDataModel = await Context.Greetings
      .Where(greeting => relationshipLevel >= greeting.MinRelationship && relationshipLevel < greeting.MaxRelationship)
      .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

    return greetingDataModel?.ToDomain();
  }

  #endregion
}
