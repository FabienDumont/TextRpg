using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class LocationMapperTests
{
  #region Methods

  [Fact]
  public void MapToDomain_ShouldMapDataModelToDomain()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "Home";

    var dataModel = new LocationDataModel
    {
      Id = id,
      Name = name,
      IsPlayerSpawn = true
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(id);
    domain.Name.Should().Be(name);
    domain.IsPlayerSpawn.Should().Be(true);
  }

  [Fact]
  public void MapToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<LocationDataModel>
    {
      new() {Id = Guid.NewGuid(), Name = "Home", IsPlayerSpawn = true},
      new() {Id = Guid.NewGuid(), Name = "Street", IsPlayerSpawn = false}
    };

    // Act
    var domainModels = dataModels.ToDomainCollection();

    // Assert
    domainModels.Should().HaveCount(2);
    domainModels[0].Id.Should().Be(dataModels[0].Id);
    domainModels[0].Name.Should().Be(dataModels[0].Name);
    domainModels[0].IsPlayerSpawn.Should().Be(dataModels[0].IsPlayerSpawn);
    domainModels[1].Id.Should().Be(dataModels[1].Id);
    domainModels[1].Name.Should().Be(dataModels[1].Name);
    domainModels[1].IsPlayerSpawn.Should().Be(dataModels[1].IsPlayerSpawn);
  }

  [Fact]
  public void MapToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "Home";

    var domain = Location.Load(id, name, true);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(id);
    dataModel.Name.Should().Be(name);
    dataModel.IsPlayerSpawn.Should().Be(true);
  }

  #endregion
}
