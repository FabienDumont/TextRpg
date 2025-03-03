using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Seeders;

/// <summary>
///   Trait data seeder.
/// </summary>
public class TraitSeeder : IDataSeeder
{
  #region Implementation of IDataSeeder

  /// <inheritdoc />
  public async Task SeedAsync(ApplicationContext context)
  {
    var traits = new Dictionary<string, TraitDataModel>();

    foreach (var name in new[]
             {
               "blunt", "carefree", "confident", "generous", "lawless", "mean",
               "polite", "rude", "selfish", "serious", "shy", "talkative"
             })
    {
      var trait = new TraitDataModel
      {
        Id = Guid.NewGuid(),
        Name = name
      };

      await context.Traits.AddAsync(trait).ConfigureAwait(false);
      traits[name] = trait;

      await context.SaveChangesAsync().ConfigureAwait(false);
    }

    var incompatiblePairs = new (string Trait, string Incompatible)[]
    {
      ("blunt", "polite"),
      ("blunt", "shy"),
      ("carefree", "serious"),
      ("carefree", "shy"),
      ("confident", "shy"),
      ("generous", "selfish"),
      ("generous", "mean"),
      ("polite", "rude"),
      ("polite", "lawless"),
      ("serious", "lawless"),
      ("shy", "talkative")
    };

    var incompatibilities = incompatiblePairs.Select(pair => new IncompatibleTraitDataModel
      {
        TraitId = traits[pair.Trait].Id,
        IncompatibleTraitId = traits[pair.Incompatible].Id
      }
    );

    await context.IncompatibleTraits.AddRangeAsync(incompatibilities).ConfigureAwait(false);
    await context.SaveChangesAsync().ConfigureAwait(false);
  }

  #endregion
}
