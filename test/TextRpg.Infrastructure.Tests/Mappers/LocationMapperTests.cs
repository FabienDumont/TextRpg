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
    const bool isAlwaysOpen = true;

    var dataModel = new LocationDataModel
    {
      Id = id,
      Name = name,
      IsAlwaysOpen = isAlwaysOpen
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    Assert.Equal(id, domain.Id);
    Assert.Equal(name, domain.Name);
    Assert.Equal(isAlwaysOpen, domain.IsAlwaysOpen);
  }

  [Fact]
  public void MapToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<LocationDataModel>
    {
      new() {Id = Guid.NewGuid(), Name = "Home", IsAlwaysOpen = true},
      new() {Id = Guid.NewGuid(), Name = "Street", IsAlwaysOpen = true}
    };

    // Act
    var domainModels = dataModels.ToDomainCollection();

    // Assert
    Assert.Equal(2, domainModels.Count);

    Assert.Equal(dataModels[0].Id, domainModels[0].Id);
    Assert.Equal(dataModels[0].Name, domainModels[0].Name);
    Assert.Equal(dataModels[0].IsAlwaysOpen, domainModels[0].IsAlwaysOpen);

    Assert.Equal(dataModels[1].Id, domainModels[1].Id);
    Assert.Equal(dataModels[1].Name, domainModels[1].Name);
    Assert.Equal(dataModels[1].IsAlwaysOpen, domainModels[1].IsAlwaysOpen);
  }

  [Fact]
  public void MapToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "Home";
    const bool isAlwaysOpen = true;

    var domain = Location.Load(id, name, isAlwaysOpen);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    Assert.Equal(id, dataModel.Id);
    Assert.Equal(name, dataModel.Name);
    Assert.Equal(isAlwaysOpen, dataModel.IsAlwaysOpen);
  }

  #endregion
}
