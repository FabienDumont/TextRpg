using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class ExplorationActionMapperTests
{
  #region Methods

  [Fact]
  public void ToDomain_ShouldMapDataModelToDomain()
  {
    // Arrange
    var id = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    var roomId = Guid.NewGuid();
    const string label = "Sleep";
    const int neededMinutes = 480;

    var dataModel = new ExplorationActionDataModel
    {
      Id = id,
      LocationId = locationId,
      RoomId = roomId,
      Label = label,
      NeededMinutes = neededMinutes
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(id);
    domain.LocationId.Should().Be(locationId);
    domain.RoomId.Should().Be(roomId);
    domain.Label.Should().Be(label);
    domain.NeededMinutes.Should().Be(neededMinutes);
  }

  [Fact]
  public void ToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<ExplorationActionDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        LocationId = Guid.NewGuid(),
        RoomId = Guid.NewGuid(),
        Label = "Sleep",
        NeededMinutes = 480
      },
      new()
      {
        Id = Guid.NewGuid(),
        LocationId = Guid.NewGuid(),
        RoomId = null,
        Label = "Nap",
        NeededMinutes = 60
      }
    };

    // Act
    var domains = dataModels.ToDomainCollection();

    // Assert
    domains.Should().HaveCount(2);
    for (var i = 0; i < domains.Count; i++)
    {
      domains[i].Id.Should().Be(dataModels[i].Id);
      domains[i].LocationId.Should().Be(dataModels[i].LocationId);
      domains[i].RoomId.Should().Be(dataModels[i].RoomId);
      domains[i].Label.Should().Be(dataModels[i].Label);
      domains[i].NeededMinutes.Should().Be(dataModels[i].NeededMinutes);
    }
  }

  [Fact]
  public void ToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    var roomId = Guid.NewGuid();
    const string label = "Sleep";
    const int neededMinutes = 480;

    var domain = ExplorationAction.Load(id, locationId, roomId, label, neededMinutes);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(id);
    dataModel.LocationId.Should().Be(locationId);
    dataModel.RoomId.Should().Be(roomId);
    dataModel.Label.Should().Be(label);
    dataModel.NeededMinutes.Should().Be(neededMinutes);
  }

  [Fact]
  public void ToDataModelCollection_ShouldMapDomainCollectionToDataModelCollection()
  {
    // Arrange
    var domains = new List<ExplorationAction>
    {
      ExplorationAction.Load(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Sleep", 480),
      ExplorationAction.Load(Guid.NewGuid(), Guid.NewGuid(), null, "Nap", 60)
    };

    // Act
    var dataModels = domains.ToDataModelCollection();

    // Assert
    dataModels.Should().HaveCount(2);
    for (var i = 0; i < dataModels.Count; i++)
    {
      dataModels[i].Id.Should().Be(domains[i].Id);
      dataModels[i].LocationId.Should().Be(domains[i].LocationId);
      dataModels[i].RoomId.Should().Be(domains[i].RoomId);
      dataModels[i].Label.Should().Be(domains[i].Label);
      dataModels[i].NeededMinutes.Should().Be(domains[i].NeededMinutes);
    }
  }

  #endregion
}
