using Microsoft.EntityFrameworkCore;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Seeders;

/// <summary>
///   Greeting data seeder.
/// </summary>
public class GreetingSeeder : IDataSeeder
{
  #region Implementation of IDataSeeder

  /// <inheritdoc />
  public async Task SeedAsync(ApplicationContext context)
  {
    var lawless = await context.Traits.FirstAsync(l => l.Name == "lawless");
    var rude = await context.Traits.FirstAsync(l => l.Name == "rude");

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
        HasTrait = lawless.Id,
        SpokenText = "Yo."
      },
      new GreetingDataModel
      {
        Id = Guid.NewGuid(),
        MinRelationship = 40,
        MaxRelationship = 60,
        HasTrait = rude.Id,
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

    await context.Greetings.AddRangeAsync(greetings).ConfigureAwait(false);
    await context.SaveChangesAsync().ConfigureAwait(false);
  }

  #endregion
}
