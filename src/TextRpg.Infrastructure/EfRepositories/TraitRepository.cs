using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for traits.
/// </summary>
public class TraitRepository(ApplicationContext context) : RepositoryBase(context), ITraitRepository
{
  #region Implementation of ITraitRepository

  /// <inheritdoc />
  public async Task<IReadOnlyCollection<Trait>> GetAllAsync(CancellationToken cancellationToken)
  {
    var userDataModels = await Context.Traits.ToListAsync(cancellationToken).ConfigureAwait(false);

    return userDataModels.ToDomainCollection();
  }

  /// <inheritdoc />
  public async Task<IReadOnlyCollection<Trait>> GetCompatibleTraitsAsync(
    IEnumerable<Guid> selectedTraitsIdsEnumerable, CancellationToken cancellationToken
  )
  {
    var selectedTraitsIds = selectedTraitsIdsEnumerable.ToList();
    if (selectedTraitsIds.Count == 0)
    {
      return await GetAllAsync(cancellationToken);
    }

    var incompatibleIds = await Context.IncompatibleTraits
      .Where(it => selectedTraitsIds.Contains(it.TraitId) || selectedTraitsIds.Contains(it.IncompatibleTraitId))
      .Select(it => selectedTraitsIds.Contains(it.TraitId) ? it.IncompatibleTraitId : it.TraitId).Distinct()
      .ToListAsync(cancellationToken);

    var excludedIds = selectedTraitsIds.Concat(incompatibleIds).ToHashSet();

    var compatibleTraits = await Context.Traits.Where(t => !excludedIds.Contains(t.Id)).ToListAsync(cancellationToken);

    return compatibleTraits.ToDomainCollection();
  }

  #endregion
}
