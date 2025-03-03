using Microsoft.EntityFrameworkCore;
using TextRpg.Application.Repositories;
using TextRpg.Domain;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.EfRepositories;

/// <summary>
///   Repository for narrations.
/// </summary>
public class NarrationRepository(ApplicationContext context) : RepositoryBase(context), INarrationRepository
{
  #region Implementation of INarrationRepository

  /// <inheritdoc />
  public async Task<Narration> GetNarrationByKeyAsync(string key, CancellationToken cancellationToken)
  {
    var dataModel = await Context.Narrations.FirstOrDefaultAsync(n => n.Key.Equals(key), cancellationToken);

    if (dataModel is null)
    {
      throw new InvalidOperationException($"No narration found for key {key}");
    }

    return dataModel.ToDomain();
  }

  #endregion
}
