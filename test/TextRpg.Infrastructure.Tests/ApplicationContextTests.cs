using Microsoft.EntityFrameworkCore;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests;

public class ApplicationContextTests
{
  #region Fields

  private readonly DbContextOptions<ApplicationContext> _options = new DbContextOptionsBuilder<ApplicationContext>()
    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

  #endregion

  #region Methods

  [Fact]
  public async Task InitializeDataAsync_ShouldNotSeed_WhenTraitsAlreadyExist()
  {
    // Arrange
    await using var context = new ApplicationContext(_options);
    context.Traits.Add(new TraitDataModel {Id = Guid.NewGuid(), Name = "Existing"});
    await context.SaveChangesAsync();

    // Act
    await context.InitializeDataAsync();

    // Assert
    var traits = await context.Traits.ToListAsync();
    traits.Should().ContainSingle();

    var incompatibilities = await context.IncompatibleTraits.ToListAsync();
    incompatibilities.Should().BeEmpty();

    var greetings = await context.Greetings.ToListAsync();
    greetings.Should().BeEmpty();

    var locations = await context.Locations.ToListAsync();
    locations.Should().BeEmpty();

    var rooms = await context.Rooms.ToListAsync();
    rooms.Should().BeEmpty();
  }

  [Fact]
  public async Task InitializeDataAsync_ShouldSeedMinimumData()
  {
    // Arrange
    await using var context = new ApplicationContext(_options);

    // Act
    await context.InitializeDataAsync();

    // Assert
    var traits = await context.Traits.ToListAsync();
    var incompatibilities = await context.IncompatibleTraits.ToListAsync();
    var greetings = await context.Greetings.ToListAsync();
    var locations = await context.Locations.ToListAsync();
    var rooms = await context.Rooms.ToListAsync();

    // Basic sanity checks
    traits.Should().NotBeEmpty();
    incompatibilities.Should().NotBeEmpty();
    greetings.Should().NotBeEmpty();
    locations.Should().NotBeEmpty();
    rooms.Should().NotBeEmpty();
  }

  #endregion
}
