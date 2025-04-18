using TextRpg.Infrastructure.JsonDataModels;

namespace TextRpg.Infrastructure.Tests.JsonDataModels;

public class GameSaveDataModelTests
{
  [Fact]
  public void GameSaveDataModel_Should_InitializeWithDefaultValues()
  {
    // Act
    var model = new GameSaveDataModel()
    {
      World = new WorldDataModel()
    };

    // Assert
    model.Id.Should().Be(Guid.Empty);
    model.Name.Should().BeEmpty();
    model.PlayerCharacterId.Should().Be(Guid.Empty);
    model.World.Should().NotBeNull();
    model.SavedAt.Should().Be(default);
  }

  [Fact]
  public void GameSaveDataModel_Should_Assign_Values_Properly()
  {
    // Arrange
    var id = Guid.NewGuid();
    var characterId = Guid.NewGuid();
    var timestamp = DateTime.UtcNow;

    var character = new CharacterDataModel
    {
      Id = characterId,
      Name = "Test",
      TraitsId = [Guid.NewGuid()]
    };

    var worldDataModel = new WorldDataModel
    {
      Id = Guid.NewGuid(),
      CurrentDate = DateTime.Now,
      Characters =
      [
        character
      ]
    };

    // Act
    var model = new GameSaveDataModel
    {
      Id = id,
      Name = "Test Save",
      PlayerCharacterId = characterId,
      World = worldDataModel,
      SavedAt = timestamp
    };

    // Assert
    model.Id.Should().Be(id);
    model.Name.Should().Be("Test Save");
    model.PlayerCharacterId.Should().Be(characterId);
    model.World.Should().BeEquivalentTo(worldDataModel);
    model.SavedAt.Should().Be(timestamp);
  }
}
