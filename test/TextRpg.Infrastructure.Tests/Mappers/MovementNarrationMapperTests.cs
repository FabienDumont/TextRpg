using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class MovementNarrationMapperTests
{
  #region Tests

  [Fact]
  public void ToDomain_ShouldMapDataModelToDomain()
  {
    // Arrange
    var id = Guid.NewGuid();
    var movementId = Guid.NewGuid();
    var text = "You walk into the street.";

    var dataModel = new MovementNarrationDataModel
    {
      Id = id,
      MovementId = movementId,
      Text = text
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(id);
    domain.MovementId.Should().Be(movementId);
    domain.Text.Should().Be(text);
  }

  [Fact]
  public void ToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<MovementNarrationDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        MovementId = Guid.NewGuid(),
        Text = "You step outside."
      },
      new()
      {
        Id = Guid.NewGuid(),
        MovementId = Guid.NewGuid(),
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
      domainModels[i].MovementId.Should().Be(dataModels[i].MovementId);
      domainModels[i].Text.Should().Be(dataModels[i].Text);
    }
  }

  [Fact]
  public void ToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    var movementId = Guid.NewGuid();
    var text = "You sneak into the alley.";

    var domain = MovementNarration.Load(id, movementId, text);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(id);
    dataModel.MovementId.Should().Be(movementId);
    dataModel.Text.Should().Be(text);
  }

  #endregion
}
