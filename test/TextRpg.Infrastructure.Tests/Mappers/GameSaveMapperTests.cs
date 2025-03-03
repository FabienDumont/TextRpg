using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class GameSaveMapperTests
{
  [Fact]
  public void ToDataModel_Should_Map_GameSave_To_GameSaveDataModel()
  {
    // Arrange
    var character = Character.Create("Player");
    character.AddTraits([Guid.NewGuid(), Guid.NewGuid()]);
    var save = GameSave.Create("Test Save", character);

    // Act
    var dataModel = save.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(save.Id);
    dataModel.Name.Should().Be(save.Name);
    dataModel.PlayerCharacterId.Should().Be(save.PlayerCharacterId);
    dataModel.Characters.Should().HaveCount(1);
    dataModel.Characters[0].Id.Should().Be(character.Id);
    dataModel.Characters[0].TraitsId.Should().BeEquivalentTo(character.TraitsId);
  }

  [Fact]
  public void ToDomain_Should_Map_GameSaveDataModel_To_GameSave()
  {
    // Arrange
    var characterId = Guid.NewGuid();
    var traitIds = new List<Guid> {Guid.NewGuid()};

    var dataModel = new GameSaveDataModel
    {
      Id = Guid.NewGuid(),
      Name = "Loaded Save",
      PlayerCharacterId = characterId,
      Characters =
      [
        new CharacterDataModel()
        {
          Id = characterId,
          Name = "Player",
          TraitsId = traitIds
        }
      ],
      SavedAt = DateTime.UtcNow
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(dataModel.Id);
    domain.Name.Should().Be(dataModel.Name);
    domain.PlayerCharacterId.Should().Be(characterId);
    domain.Characters.Should().HaveCount(1);
    domain.PlayerCharacter.Id.Should().Be(characterId);
  }

  [Fact]
  public void ToDomainCollection_Should_Map_List_Of_DataModels()
  {
    // Arrange
    var id1 = Guid.NewGuid();
    var id2 = Guid.NewGuid();

    var dataModels = new List<GameSaveDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        Name = "Save A",
        PlayerCharacterId = id1,
        Characters = [new CharacterDataModel {Id = id1, Name = "CharA"}]
      },
      new()
      {
        Id = Guid.NewGuid(),
        Name = "Save B",
        PlayerCharacterId = id2,
        Characters = [new CharacterDataModel {Id = id2, Name = "CharB"}]
      }
    };

    // Act
    var domainSaves = dataModels.ToDomainCollection();

    // Assert
    domainSaves.Should().HaveCount(2);
    domainSaves[0].Name.Should().Be("Save A");
    domainSaves[1].Name.Should().Be("Save B");
  }
}
