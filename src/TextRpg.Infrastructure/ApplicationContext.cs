using Microsoft.EntityFrameworkCore;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure;

public class ApplicationContext : DbContext
{
  #region Properties

  public virtual DbSet<TraitDataModel> Traits { get; init; }
  public virtual DbSet<IncompatibleTraitDataModel> IncompatibleTraits { get; init; }
  public virtual DbSet<GreetingDataModel> Greetings { get; init; }

  #endregion

  #region Ctors

  public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
  {
  }

  public ApplicationContext()
  {
  }

  #endregion

  #region Methods

  public async Task InitializeDataAsync()
  {
    if (await Traits.AnyAsync().ConfigureAwait(false)) return;

    var traits = await SeedTraitsAsync();
    await SeedIncompatibleTraitsAsync(traits);
    await SeedGreetingsAsync(traits);
  }

  private async Task<Dictionary<string, TraitDataModel>> SeedTraitsAsync()
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

      await Traits.AddAsync(trait).ConfigureAwait(false);
      traits[name] = trait;

      await SaveChangesAsync().ConfigureAwait(false); // You can batch this if you want
    }

    return traits;
  }

  private async Task SeedIncompatibleTraitsAsync(Dictionary<string, TraitDataModel> traits)
  {
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

    var incompatibilities = incompatiblePairs.Select(
      pair => new IncompatibleTraitDataModel
      {
        TraitId = traits[pair.Trait].Id,
        IncompatibleTraitId = traits[pair.Incompatible].Id
      }
    );

    await IncompatibleTraits.AddRangeAsync(incompatibilities).ConfigureAwait(false);
    await SaveChangesAsync().ConfigureAwait(false);
  }

  private async Task SeedGreetingsAsync(Dictionary<string, TraitDataModel> traits)
  {
    var greetings = new[]
    {
      new GreetingDataModel
      {
        Id = Guid.NewGuid(),
        MinRelationship = 40,
        MaxRelationship = 60,
        HasTrait = null,
        SpokenText = "Hello."
      },
      new GreetingDataModel
      {
        Id = Guid.NewGuid(),
        MinRelationship = 40,
        MaxRelationship = 60,
        HasTrait = traits["lawless"].Id,
        SpokenText = "Yo."
      },
      new GreetingDataModel
      {
        Id = Guid.NewGuid(),
        MinRelationship = 40,
        MaxRelationship = 60,
        HasTrait = traits["rude"].Id,
        SpokenText = "What do you want?"
      },
      new GreetingDataModel
      {
        Id = Guid.NewGuid(),
        MinRelationship = 60,
        MaxRelationship = null,
        HasTrait = null,
        SpokenText = "Hey friend!"
      },
      new GreetingDataModel
      {
        Id = Guid.NewGuid(),
        MinRelationship = 20,
        MaxRelationship = 40,
        HasTrait = null,
        SpokenText = "It's you, again..."
      },
      new GreetingDataModel
      {
        Id = Guid.NewGuid(),
        MinRelationship = 0,
        MaxRelationship = 20,
        HasTrait = null,
        SpokenText = "Leave me alone!"
      }
    };

    await Greetings.AddRangeAsync(greetings).ConfigureAwait(false);
    await SaveChangesAsync().ConfigureAwait(false);
  }

  #endregion
}
