using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class CharacterMapperTests
{
  [Fact]
  public void ToDomain_Should_Map_CharacterDataModel_To_Character()
  {
    // Arrange
    var id = Guid.NewGuid();
    var dataModel = new CharacterDataModel
    {
      Id = id,
      Name = "Guts",
      TraitsId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
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
      new() { Id = Guid.NewGuid(), Name = "A" },
      new() { Id = Guid.NewGuid(), Name = "B" }
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
    var character = Character.Create("Casca", 18, BiologicalSex.Male);
    var traits = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
    character.AddTraits(traits);

    // Act
    var dataModel = character.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(character.Id);
    dataModel.Name.Should().Be(character.Name);
    dataModel.TraitsId.Should().BeEquivalentTo(traits);
  }

  [Fact]
  public void ToDataModelCollection_Should_Map_List_Of_Characters()
  {
    // Arrange
    var c1 = Character.Create("Skull Knight", 18, BiologicalSex.Male);
    var c2 = Character.Create("Farnese", 18, BiologicalSex.Male);

    var list = new List<Character> { c1, c2 };

    // Act
    var result = list.ToDataModelCollection();

    // Assert
    result.Should().HaveCount(2);
    result[0].Name.Should().Be("Skull Knight");
    result[1].Name.Should().Be("Farnese");
  }
}
