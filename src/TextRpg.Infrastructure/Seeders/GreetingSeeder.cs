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
    var lawless = await context.Traits.FirstAsync(t => t.Name == "lawless").ConfigureAwait(false);
    var rude = await context.Traits.FirstAsync(t => t.Name == "rude").ConfigureAwait(false);

    var hello = new GreetingDataModel
    {
      Id = Guid.NewGuid(),
      SpokenText = "Hello."
    };

    var yo = new GreetingDataModel
    {
      Id = Guid.NewGuid(),
      SpokenText = "Yo."
    };

    var whatDoYouWant = new GreetingDataModel
    {
      Id = Guid.NewGuid(),
      SpokenText = "What do you want?"
    };

    var heyFriend = new GreetingDataModel
    {
      Id = Guid.NewGuid(),
      SpokenText = "Hey friend!"
    };

    var itsYouAgain = new GreetingDataModel
    {
      Id = Guid.NewGuid(),
      SpokenText = "It's you, again..."
    };

    var leaveMeAlone = new GreetingDataModel
    {
      Id = Guid.NewGuid(),
      SpokenText = "Leave me alone!"
    };

    var greetings = new[]
    {
      hello,
      yo,
      whatDoYouWant,
      heyFriend,
      itsYouAgain,
      leaveMeAlone
    };

    var conditions = new[]
    {
      new ConditionDataModel
      {
        Id = Guid.NewGuid(),
        ContextType = "Greeting",
        ContextId = yo.Id,
        ConditionType = "HasTrait",
        OperandLeft = lawless.Id.ToString(),
        Operator = "=",
        OperandRight = "true",
        Negate = false
      },
      new ConditionDataModel
      {
        Id = Guid.NewGuid(),
        ContextType = "Greeting",
        ContextId = whatDoYouWant.Id,
        ConditionType = "HasTrait",
        OperandLeft = rude.Id.ToString(),
        Operator = "=",
        OperandRight = "true",
        Negate = false
      }
    };

    await context.Greetings.AddRangeAsync(greetings).ConfigureAwait(false);
    await context.Conditions.AddRangeAsync(conditions).ConfigureAwait(false);
    await context.SaveChangesAsync().ConfigureAwait(false);
  }

  #endregion
}
