using TextRpg.Domain;

namespace TextRpg.Application.Services;

public interface ITraitService
{
  Task<IReadOnlyCollection<Trait>> GetAllTraitsAsync(CancellationToken cancellationToken);

  Task<IReadOnlyCollection<Trait>> GetCompatibleTraitsAsync(
    IEnumerable<Guid> selectedTraitsIds, CancellationToken cancellationToken
  );
}
