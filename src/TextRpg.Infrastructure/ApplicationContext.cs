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
  ///   Represents all locations' opening hours in the world.
  /// </summary>
  public virtual DbSet<LocationOpeningHoursDataModel> LocationOpeningHours { get; init; }

  /// <summary>
  ///   Represents rooms inside locations.
  /// </summary>
  public virtual DbSet<RoomDataModel> Rooms { get; init; }

  /// <summary>
  ///   Represents movements between locations or rooms.
  /// </summary>
  public virtual DbSet<MovementDataModel> Movements { get; init; }

  /// <summary>
  ///   Represents narration texts tied to movements.
  /// </summary>
  public virtual DbSet<MovementNarrationDataModel> MovementNarrations { get; init; }

  /// <summary>
  ///   Represents narration texts.
  /// </summary>
  public virtual DbSet<NarrationDataModel> Narrations { get; init; }

  /// <summary>
  ///   Represents narration texts.
  /// </summary>
  public virtual DbSet<ExplorationActionDataModel> ExplorationActions { get; init; }

  #endregion

  #region Ctors

  /// <summary>
  ///   Initializes a new instance of <see cref="ApplicationContext" /> with options.
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
    if (await Traits.AnyAsync().ConfigureAwait(false))
    {
      return;
    }

    var traits = await SeedTraitsAsync();
    await SeedIncompatibleTraitsAsync(traits);
    await SeedGreetingsAsync(traits);
    await SeedLocationsAsync();
    await SeedNarrationsAsync();
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
    var collegeId = Guid.NewGuid();

    await Locations.AddRangeAsync(
      new LocationDataModel {Id = homeId, Name = "Home", IsAlwaysOpen = true},
      new LocationDataModel {Id = streetId, Name = "Street", IsAlwaysOpen = true},
      new LocationDataModel {Id = collegeId, Name = "College", IsAlwaysOpen = false}
    );

    var bedroomId = Guid.NewGuid();
    var livingRoomId = Guid.NewGuid();

    await Rooms.AddRangeAsync(
      new RoomDataModel {Id = bedroomId, LocationId = homeId, Name = "Bedroom", IsPlayerSpawn = true},
      new RoomDataModel {Id = livingRoomId, LocationId = homeId, Name = "Living Room", IsPlayerSpawn = false}
    );

    await SaveChangesAsync().ConfigureAwait(false);

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
    var movementStreetToCollege = new MovementDataModel
    {
      Id = Guid.NewGuid(),
      FromLocationId = streetId,
      FromRoomId = null,
      ToLocationId = collegeId,
      ToRoomId = null,
      RequiredItemId = null
    };
    var movementCollegeToStreet = new MovementDataModel
    {
      Id = Guid.NewGuid(),
      FromLocationId = collegeId,
      FromRoomId = null,
      ToLocationId = streetId,
      ToRoomId = null,
      RequiredItemId = null
    };

    await Movements.AddRangeAsync(
      movementHomeLivingRoomToHomeBedroom, movementHomeBedroomToHomeLivingRoom, movementHomeLivingRoomToStreet,
      movementStreetToHomeLivingRoom, movementStreetToCollege, movementCollegeToStreet
    );

    await SaveChangesAsync().ConfigureAwait(false);

    await ExplorationActions.AddRangeAsync(
      new ExplorationActionDataModel
      {
        Id = Guid.NewGuid(),
        LocationId = homeId,
        RoomId = bedroomId,
        Label = "Sleep for 8 hours",
        NeededMinutes = 480
      }
    );

    var openingHours =
      new[] {DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday}
        .Select(day => new LocationOpeningHoursDataModel
          {
            Id = Guid.NewGuid(),
            LocationId = collegeId,
            DayOfWeek = day,
            OpensAt = new TimeSpan(8, 0, 0),
            ClosesAt = new TimeSpan(18, 0, 0)
          }
        ).ToList();

    await LocationOpeningHours.AddRangeAsync(openingHours);

    const string streetMovementNarration = "You step outside to the street.";

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
        Text = streetMovementNarration
      }, new MovementNarrationDataModel
      {
        Id = Guid.NewGuid(),
        MovementId = movementStreetToHomeLivingRoom.Id,
        Text = "You return to your home."
      }, new MovementNarrationDataModel
      {
        Id = Guid.NewGuid(),
        MovementId = movementStreetToCollege.Id,
        Text = "You enter the college."
      }, new MovementNarrationDataModel
      {
        Id = Guid.NewGuid(),
        MovementId = movementCollegeToStreet.Id,
        Text = streetMovementNarration
      }
    );

    await SaveChangesAsync().ConfigureAwait(false);
  }

  /// <summary>
  ///   Seeds default narration texts.
  /// </summary>
  private async Task SeedNarrationsAsync()
  {
    await Narrations.AddAsync(
      new NarrationDataModel
      {
        Id = Guid.NewGuid(),
        Key = "GameIntro",
        Text =
          "You wake up in your bedroom. The faint hum of the city leaks through the closed windows, mixing with the ticking of a wall clock. Nothing feels out of place—yet, something nags at the edge of your mind. Today’s the day things change."
      }
    );
    await SaveChangesAsync();
  }

  #endregion
}
