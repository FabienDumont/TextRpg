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
      Name = name
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(id);
    domain.Name.Should().Be(name);
  }

  [Fact]
  public void MapToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<LocationDataModel>
    {
      new() {Id = Guid.NewGuid(), Name = "Home"},
      new() {Id = Guid.NewGuid(), Name = "Street"}
    };

    // Act
    var domainModels = dataModels.ToDomainCollection();

    // Assert
    domainModels.Should().HaveCount(2);
    domainModels[0].Id.Should().Be(dataModels[0].Id);
    domainModels[0].Name.Should().Be(dataModels[0].Name);
    domainModels[1].Id.Should().Be(dataModels[1].Id);
    domainModels[1].Name.Should().Be(dataModels[1].Name);
  }

  [Fact]
  public void MapToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "Home";

    var domain = Location.Load(id, name);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(id);
    dataModel.Name.Should().Be(name);
  }

  #endregion
}
