using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Domain.Tests;

public class GameSaveTests
{
  [Fact]
  public void Create_ShouldInitializeCorrectly()
  {
    // Arrange
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var world = World.Create(DateTime.Now, [playerCharacter]);

    // Act
    var save = GameSave.Create("New Save", playerCharacter, world);

    // Assert
    save.Should().NotBeNull();
    save.Id.Should().NotBe(Guid.Empty);
    save.Name.Should().Be("New Save");
    save.PlayerCharacterId.Should().Be(playerCharacter.Id);
    save.World.Should().BeEquivalentTo(world);
    save.PlayerCharacter.Should().Be(playerCharacter);
    save.SavedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
  }

  [Fact]
  public void Load_ShouldInitializeCorrectly()
  {
    // Arrange
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var id = Guid.NewGuid();
    var name = "Save 1";
    var world = World.Create(DateTime.Now, [playerCharacter]);

    // Act
    var save = GameSave.Load(id, name, playerCharacter.Id, world);

    // Assert
    save.Id.Should().Be(id);
    save.Name.Should().Be(name);
    save.PlayerCharacterId.Should().Be(playerCharacter.Id);
    save.World.Should().BeEquivalentTo(world);
    save.PlayerCharacter.Should().Be(playerCharacter);
  }

  [Fact]
  public void Load_ShouldThrow_WhenPlayerCharacterNotFound()
  {
    // Arrange
    var id = Guid.NewGuid();
    var name = "Save X";
    var wrongPlayerId = Guid.NewGuid();
    var world = World.Create(DateTime.Now, []);

    // Act
    var act = () => GameSave.Load(id, name, wrongPlayerId, world);

    // Assert
    act.Should().Throw<InvalidOperationException>().WithMessage("Player character not found in character list.");
  }

  [Fact]
  public void Create_ShouldThrow_WhenPlayerIsNull()
  {
    // Arrange
    var world = World.Create(DateTime.Now, []);

    // Act
    var act = () => GameSave.Create("Save Null", null!, world);

    // Assert
    act.Should().Throw<ArgumentNullException>().WithParameterName("playerCharacter");
  }
}
