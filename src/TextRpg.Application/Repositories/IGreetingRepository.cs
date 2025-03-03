using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

public interface IGreetingRepository
{
  Task<Greeting?> GetByRelationshipLevelAsync(
    int relationshipLevel, IEnumerable<Trait> traits, CancellationToken cancellationToken
  );
}
