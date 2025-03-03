using Microsoft.EntityFrameworkCore;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure;

public class ApplicationContext : DbContext
{
  #region Properties

  /// <summary>
  ///   Represents all traits in the database.
  /// </summary>
  public virtual DbSet<TraitDataModel> Traits { get; init; }

  /// <summary>
  ///   Represents pairs of incompatible traits.
  /// </summary>
  public virtual DbSet<IncompatibleTraitDataModel> IncompatibleTraits { get; init; }

  /// <summary>
  ///   Represents all greetings used in NPC interactions.
  /// </summary>
  public virtual DbSet<GreetingDataModel> Greetings { get; init; }

  /// <summary>
  ///   Represents all locations in the world.
  /// </summary>
  public virtual DbSet<LocationDataModel> Locations { get; init; }

  /// <summary>
  ///   Represents rooms inside locations.
  /// </summary>
  public virtual DbSet<RoomDataModel> Rooms { get; init; }

  /// <summary>
  ///   Represents movements between locations or rooms.
  /// </summary>
  public virtual DbSet<MovementDataModel> Movements { get; init; }

  /// <summary>
  ///   Represents narration text tied to a movement.
  /// </summary>
  public virtual DbSet<MovementNarrationDataModel> MovementNarrations { get; init; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Initializes a new instance of <see cref="ApplicationContext"/> with options.
  /// </summary>
  public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
  {
  }

  /// <summary>
  ///   Parameterless constructor for design-time tools and migrations.
  /// </summary>
  public ApplicationContext()
  {
  }

  #endregion

  #region Methods

  /// <summary>
  ///   Seeds the database with initial data if not already populated.
  /// </summary>
  public async Task InitializeDataAsync()
  {
    if (await Traits.AnyAsync().ConfigureAwait(false)) return;

    var traits = await SeedTraitsAsync();
    await SeedIncompatibleTraitsAsync(traits);
    await SeedGreetingsAsync(traits);
    await SeedLocationsAsync();
  }

  /// <summary>
  ///   Seeds predefined traits into the database.
  /// </summary>
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

  /// <summary>
  ///   Seeds known incompatible trait pairs.
  /// </summary>
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

    var incompatibilities = incompatiblePairs.Select(pair => new IncompatibleTraitDataModel
      {
        TraitId = traits[pair.Trait].Id,
        IncompatibleTraitId = traits[pair.Incompatible].Id
      }
    );

    await IncompatibleTraits.AddRangeAsync(incompatibilities).ConfigureAwait(false);
    await SaveChangesAsync().ConfigureAwait(false);
  }

  /// <summary>
  ///   Seeds default greetings, some based on traits.
  /// </summary>
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

  /// <summary>
  ///   Seeds default locations, rooms, movements, and narration text.
  /// </summary>
  private async Task SeedLocationsAsync()
  {
    var homeId = Guid.NewGuid();
    var streetId = Guid.NewGuid();

    await Locations.AddRangeAsync(
      new LocationDataModel {Id = homeId, Name = "Home", IsPlayerSpawn = true},
      new LocationDataModel {Id = streetId, Name = "Street", IsPlayerSpawn = false}
    );

    var bedroomId = Guid.NewGuid();
    var livingRoomId = Guid.NewGuid();

    await Rooms.AddRangeAsync(
      new RoomDataModel {Id = bedroomId, LocationId = homeId, Name = "Bedroom", IsEntryPoint = false},
      new RoomDataModel {Id = livingRoomId, LocationId = homeId, Name = "Living Room", IsEntryPoint = true}
    );

    var movementHomeLivingRoomToHomeBedroom = new MovementDataModel
    {
      Id = Guid.NewGuid(),
      FromLocationId = homeId,
      FromRoomId = livingRoomId,
      ToLocationId = homeId,
      ToRoomId = bedroomId,
      RequiredItemId = null
    };
    var movementHomeBedroomToHomeLivingRoom = new MovementDataModel
    {
      Id = Guid.NewGuid(),
      FromLocationId = homeId,
      FromRoomId = bedroomId,
      ToLocationId = homeId,
      ToRoomId = livingRoomId,
      RequiredItemId = null
    };
    var movementHomeLivingRoomToStreet = new MovementDataModel
    {
      Id = Guid.NewGuid(),
      FromLocationId = homeId,
      FromRoomId = livingRoomId,
      ToLocationId = streetId,
      ToRoomId = null,
      RequiredItemId = null
    };
    var movementStreetToHomeLivingRoom = new MovementDataModel
    {
      Id = Guid.NewGuid(),
      FromLocationId = streetId,
      FromRoomId = null,
      ToLocationId = homeId,
      ToRoomId = livingRoomId,
      RequiredItemId = null
    };

    await Movements.AddRangeAsync(
      movementHomeLivingRoomToHomeBedroom, movementHomeBedroomToHomeLivingRoom, movementHomeLivingRoomToStreet,
      movementStreetToHomeLivingRoom
    );

    await MovementNarrations.AddRangeAsync(
      new MovementNarrationDataModel
      {
        Id = Guid.NewGuid(),
        MovementId = movementHomeLivingRoomToHomeBedroom.Id,
        Text = "You enter your bedroom."
      }, new MovementNarrationDataModel
      {
        Id = Guid.NewGuid(),
        MovementId = movementHomeBedroomToHomeLivingRoom.Id,
        Text = "You walk into the living room."
      }, new MovementNarrationDataModel
      {
        Id = Guid.NewGuid(),
        MovementId = movementHomeLivingRoomToStreet.Id,
        Text = "You step outside to the street."
      }, new MovementNarrationDataModel
      {
        Id = Guid.NewGuid(),
        MovementId = movementStreetToHomeLivingRoom.Id,
        Text = "You return to your home."
      }
    );

    await SaveChangesAsync().ConfigureAwait(false);
  }

  #endregion
}
