using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class NarrationMapperTests
{
  #region Tests

  [Fact]
  public void ToDomain_ShouldMapDataModelToDomain()
  {
    // Arrange
    var id = Guid.NewGuid();
    var key = "intro_scene";
    var text = "You wake up in a strange room.";

    var dataModel = new NarrationDataModel
    {
      Id = id,
      Key = key,
      Text = text
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(id);
    domain.Key.Should().Be(key);
    domain.Text.Should().Be(text);
  }

  [Fact]
  public void ToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<NarrationDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        Key = "scene_1",
        Text = "You step outside."
      },
      new()
      {
        Id = Guid.NewGuid(),
        Key = "scene_2",
        Text = "You return to your home."
      }
    };

    // Act
    var domainModels = dataModels.ToDomainCollection();

    // Assert
    domainModels.Should().HaveCount(2);
    for (var i = 0; i < domainModels.Count; i++)
    {
      domainModels[i].Id.Should().Be(dataModels[i].Id);
      domainModels[i].Key.Should().Be(dataModels[i].Key);
      domainModels[i].Text.Should().Be(dataModels[i].Text);
    }
  }

  [Fact]
  public void ToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    var key = "alley_scene";
    var text = "You sneak into the alley.";

    var domain = Narration.Load(id, key, text);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(id);
    dataModel.Key.Should().Be(key);
    dataModel.Text.Should().Be(text);
  }

  #endregion
}
