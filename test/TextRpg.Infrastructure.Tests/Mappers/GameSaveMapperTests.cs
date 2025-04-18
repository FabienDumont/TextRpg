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
    var player = Character.Create("Player", 18, BiologicalSex.Male);
    player.AddTraits([Guid.NewGuid(), Guid.NewGuid()]);
    var world = World.Create(DateTime.Now, [player]);
    var save = GameSave.Create("Test Save", player, world);

    // Act
    var dataModel = save.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(save.Id);
    dataModel.Name.Should().Be(save.Name);
    dataModel.Name.Should().Be(save.Name);
    dataModel.PlayerCharacterId.Should().Be(save.PlayerCharacterId);
  }

  [Fact]
  public void ToDomain_Should_Map_GameSaveDataModel_To_GameSave()
  {
    // Arrange
    var characterId = Guid.NewGuid();
    var traitIds = new List<Guid> {Guid.NewGuid()};
    var worldDataModel = new WorldDataModel
    {
      Id = Guid.NewGuid(),
      CurrentDate = DateTime.Now,
      Characters =
      [
        new CharacterDataModel
        {
          Id = characterId,
          Name = "Player",
          TraitsId = traitIds
        }
      ]
    };

    var dataModel = new GameSaveDataModel
    {
      Id = Guid.NewGuid(),
      Name = "Loaded Save",
      World = worldDataModel,
      PlayerCharacterId = characterId,
      SavedAt = DateTime.UtcNow
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(dataModel.Id);
    domain.Name.Should().Be(dataModel.Name);
    domain.PlayerCharacterId.Should().Be(characterId);
    domain.World.Should().BeEquivalentTo(worldDataModel);
    domain.PlayerCharacter.Id.Should().Be(characterId);
  }

  [Fact]
  public void ToDomainCollection_Should_Map_List_Of_DataModels()
  {
    // Arrange
    var id1 = Guid.NewGuid();
    var id2 = Guid.NewGuid();

    var world1DataModel = new WorldDataModel
    {
      Id = Guid.NewGuid(),
      CurrentDate = DateTime.Now,
      Characters =
      [
        new CharacterDataModel
        {
          Id = id1,
          Name = "CharA"
        }
      ]
    };

    var world2DataModel = new WorldDataModel
    {
      Id = Guid.NewGuid(),
      CurrentDate = DateTime.Now,
      Characters =
      [
        new CharacterDataModel
        {
          Id = id2,
          Name = "CharB"
        }
      ]
    };

    var dataModels = new List<GameSaveDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        Name = "Save A",
        PlayerCharacterId = id1,
        World = world1DataModel
      },
      new()
      {
        Id = Guid.NewGuid(),
        Name = "Save B",
        PlayerCharacterId = id2,
        World = world2DataModel
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
