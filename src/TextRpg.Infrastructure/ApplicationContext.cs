using Microsoft.EntityFrameworkCore;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Seeders;

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
  ///   Represents exploration actions.
  /// </summary>
  public virtual DbSet<ExplorationActionDataModel> ExplorationActions { get; init; }

  /// <summary>
  ///   Represents exploration actions' results.
  /// </summary>
  public virtual DbSet<ExplorationActionResultDataModel> ExplorationActionResults { get; init; }

  /// <summary>
  ///   Represents exploration action results' narrations.
  /// </summary>
  public virtual DbSet<ExplorationActionResultNarrationDataModel> ExplorationActionResultNarrations { get; init; }

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

    var seeders = new IDataSeeder[]
    {
      new TraitSeeder(),
      new GreetingSeeder(),
      new LocationSeeder(),
      new ExplorationActionSeeder(),
      new NarrationSeeder()
    };

    foreach (var seeder in seeders)
    {
      await seeder.SeedAsync(this);
      await SaveChangesAsync();
    }
  }

  #endregion
}
