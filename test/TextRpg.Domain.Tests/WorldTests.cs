using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Domain.Tests;

public class WorldTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitialize()
  {
    // Arrange
    var currentDate = new DateTime(2025, 4, 24, 8, 0, 0);
    var characters = new List<Character>();

    // Act
    var world = World.Create(currentDate, characters);

    // Assert
    world.Should().NotBeNull();
    world.Id.Should().NotBe(Guid.Empty);
    world.CurrentDate.Should().Be(currentDate);
    world.Characters.Should().BeEquivalentTo(characters);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var currentDate = new DateTime(2025, 4, 24, 8, 0, 0);
    var characters = new List<Character>();

    // Act
    var world = World.Load(id, currentDate, characters);

    // Assert
    world.Id.Should().Be(id);
    world.CurrentDate.Should().Be(currentDate);
    world.Characters.Should().BeEquivalentTo(characters);
  }

  [Fact]
  public void AddCharacter_ShouldAddCharacterToWorld()
  {
    // Arrange
    var world = World.Create(DateTime.UtcNow, []);
    var character = CharacterHelper.GetRandomCharacter();

    // Act
    world.AddCharacter(character);

    // Assert
    world.Characters.Should().Contain(character);
  }

  [Fact]
  public void AdvanceTime_ShouldUpdateCurrentDate()
  {
    // Arrange
    var initialDate = new DateTime(2025, 4, 24, 8, 0, 0);
    var world = World.Create(initialDate, []);
    var minutes = 120; // advance 2 hours

    // Act
    world.AdvanceTime(minutes);

    // Assert
    world.CurrentDate.Should().Be(initialDate.AddMinutes(minutes));
  }

  #endregion
}
