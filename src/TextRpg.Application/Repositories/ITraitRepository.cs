using TextRpg.Domain;

namespace TextRpg.Application.Repositories;

public interface ITraitRepository
{
  Task<IReadOnlyCollection<Trait>> GetAllAsync(CancellationToken cancellationToken);

  Task<IReadOnlyCollection<Trait>> GetCompatibleTraitsAsync(
    IEnumerable<Guid> selectedTraitsIdsEnumerable, CancellationToken cancellationToken
  );
}
