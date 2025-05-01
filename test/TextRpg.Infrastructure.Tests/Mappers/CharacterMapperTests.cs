using TextRpg.Domain;
using TextRpg.Domain.Tests.Helpers;
using TextRpg.Infrastructure.JsonDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class CharacterMapperTests
{
  #region Methods

  [Fact]
  public void ToDomain_Should_Map_CharacterDataModel_To_Character()
  {
    // Arrange
    var id = Guid.NewGuid();
    var dataModel = new CharacterDataModel
    {
      Id = id,
      Name = "Guts",
      TraitsId = [Guid.NewGuid(), Guid.NewGuid()]
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(dataModel.Id);
    domain.Name.Should().Be(dataModel.Name);
    domain.TraitsId.Should().BeEquivalentTo(dataModel.TraitsId);
  }

  [Fact]
  public void ToDomainCollection_Should_Map_List_Of_DataModels()
  {
    // Arrange
    var list = new List<CharacterDataModel>
    {
      new() {Id = Guid.NewGuid(), Name = "A"},
      new() {Id = Guid.NewGuid(), Name = "B"}
    };

    // Act
    var result = list.ToDomainCollection();

    // Assert
    result.Should().HaveCount(2);
    result[0].Name.Should().Be("A");
    result[1].Name.Should().Be("B");
  }

  [Fact]
  public void ToDataModel_Should_Map_Character_To_DataModel()
  {
    // Arrange
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var traits = new List<Guid> {Guid.NewGuid(), Guid.NewGuid()};
    playerCharacter.AddTraits(traits);

    // Act
    var dataModel = playerCharacter.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(playerCharacter.Id);
    dataModel.Name.Should().Be(playerCharacter.Name);
    dataModel.TraitsId.Should().BeEquivalentTo(traits);
  }

  [Fact]
  public void ToDataModelCollection_Should_Map_List_Of_Characters()
  {
    // Arrange
    var c1 = CharacterHelper.GetRandomCharacter();
    var c2 = CharacterHelper.GetRandomCharacter();

    var list = new List<Character> {c1, c2};

    // Act
    var result = list.ToDataModelCollection();

    // Assert
    result.Should().HaveCount(2);
    result[0].Name.Should().Be(c1.Name);
    result[1].Name.Should().Be(c2.Name);
  }

  #endregion
}
