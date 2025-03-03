using TextRpg.Application.Repositories;
using TextRpg.Domain;

namespace TextRpg.Application.Services;

public class TraitService(ITraitRepository traitRepository) : ITraitService
{
  #region Implementation of ITraitService

  public async Task<IReadOnlyCollection<Trait>> GetAllTraitsAsync(CancellationToken cancellationToken)
  {
    return await traitRepository.GetAllAsync(cancellationToken);
  }

  public async Task<IReadOnlyCollection<Trait>> GetCompatibleTraitsAsync(
    IEnumerable<Guid> selectedTraitsIds, CancellationToken cancellationToken
  )
  {
    return await traitRepository.GetCompatibleTraitsAsync(selectedTraitsIds, cancellationToken);
  }

  #endregion
}
