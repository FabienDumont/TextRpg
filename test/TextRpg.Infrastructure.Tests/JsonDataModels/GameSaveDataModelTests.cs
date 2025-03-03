using TextRpg.Infrastructure.JsonDataModels;

namespace TextRpg.Infrastructure.Tests.JsonDataModels;

public class GameSaveDataModelTests
{
  [Fact]
  public void GameSaveDataModel_Should_InitializeWithDefaultValues()
  {
    // Act
    var model = new GameSaveDataModel();

    // Assert
    model.Id.Should().Be(Guid.Empty);
    model.Name.Should().BeEmpty();
    model.PlayerCharacterId.Should().Be(Guid.Empty);
    model.Characters.Should().NotBeNull().And.BeEmpty();
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

    // Act
    var model = new GameSaveDataModel
    {
      Id = id,
      Name = "Test Save",
      PlayerCharacterId = characterId,
      Characters = [character],
      SavedAt = timestamp
    };

    // Assert
    model.Id.Should().Be(id);
    model.Name.Should().Be("Test Save");
    model.PlayerCharacterId.Should().Be(characterId);
    model.Characters.Should().ContainSingle().Which.Should().Be(character);
    model.SavedAt.Should().Be(timestamp);
  }
}
