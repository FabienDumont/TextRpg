using Microsoft.EntityFrameworkCore;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests;

public class ApplicationContextTests
{
  private readonly DbContextOptions<ApplicationContext> _options = new DbContextOptionsBuilder<ApplicationContext>()
    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

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
  }

  [Fact]
  public async Task InitializeDataAsync_ShouldSeedTraitsIncompatibilitiesAndGreetings_WhenEmpty()
  {
    // Arrange
    await using var context = new ApplicationContext(_options);

    // Act
    await context.InitializeDataAsync();

    // Assert
    var traits = await context.Traits.ToListAsync();
    var incompatibilities = await context.IncompatibleTraits.ToListAsync();
    var greetings = await context.Greetings.ToListAsync();

    traits.Should().HaveCount(12);
    incompatibilities.Should().HaveCount(11);
    greetings.Should().HaveCount(6);

    // Spot-check
    traits.Should().Contain(t => t.Name == "shy");
    greetings.Should().Contain(g => g.SpokenText == "Hey friend!");
  }
}
