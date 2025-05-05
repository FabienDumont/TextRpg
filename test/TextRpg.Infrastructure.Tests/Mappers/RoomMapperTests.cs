using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class RoomMapperTests
{
  #region Methods

  [Fact]
  public void MapToDomain_ShouldMapDataModelToDomain()
  {
    // Arrange
    var id = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    const string name = "Bedroom";

    var dataModel = new RoomDataModel
    {
      Id = id,
      LocationId = locationId,
      Name = name
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(id);
    domain.LocationId.Should().Be(locationId);
    domain.Name.Should().Be(name);
  }

  [Fact]
  public void MapToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<RoomDataModel>
    {
      new() {Id = Guid.NewGuid(), LocationId = Guid.NewGuid(), Name = "Bedroom"},
      new() {Id = Guid.NewGuid(), LocationId = Guid.NewGuid(), Name = "Living room"}
    };

    // Act
    var domainModels = dataModels.ToDomainCollection();

    // Assert
    domainModels.Should().HaveCount(2);
    domainModels[0].Id.Should().Be(dataModels[0].Id);
    domainModels[0].LocationId.Should().Be(dataModels[0].LocationId);
    domainModels[0].Name.Should().Be(dataModels[0].Name);
    domainModels[1].Id.Should().Be(dataModels[1].Id);
    domainModels[1].LocationId.Should().Be(dataModels[1].LocationId);
    domainModels[1].Name.Should().Be(dataModels[1].Name);
  }

  [Fact]
  public void MapToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    const string name = "Bedroom";

    var domain = Room.Load(id, locationId, name, true);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(id);
    dataModel.Name.Should().Be(name);
    dataModel.IsPlayerSpawn.Should().Be(true);
  }

  #endregion
}
