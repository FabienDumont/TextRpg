using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class GreetingMapperTests
{
  #region Methods

  [Fact]
  public void MapToDomain_ShouldMapDataModelToDomain()
  {
    // Arrange
    var id = Guid.NewGuid();
    int? minRel = 10;
    int? maxRel = 80;
    Guid? hasTrait = Guid.NewGuid();
    const string spokenText = "Yo, what’s up?";

    var dataModel = new GreetingDataModel
    {
      Id = id,
      MinRelationship = minRel,
      MaxRelationship = maxRel,
      HasTrait = hasTrait,
      SpokenText = spokenText
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    Assert.Equal(id, domain.Id);
    Assert.Equal(minRel, domain.MinRelationship);
    Assert.Equal(maxRel, domain.MaxRelationship);
    Assert.Equal(hasTrait, domain.HasTrait);
    Assert.Equal(spokenText, domain.SpokenText);
  }

  [Fact]
  public void MapToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<GreetingDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        MinRelationship = 0,
        MaxRelationship = 50,
        HasTrait = Guid.NewGuid(),
        SpokenText = "Hey"
      },
      new()
      {
        Id = Guid.NewGuid(),
        MinRelationship = null,
        MaxRelationship = null,
        HasTrait = null,
        SpokenText = "Hi there"
      }
    };

    // Act
    var domainModels = dataModels.ToDomainCollection();

    // Assert
    Assert.Equal(2, domainModels.Count);

    for (var i = 0; i < domainModels.Count; i++)
    {
      Assert.Equal(dataModels[i].Id, domainModels[i].Id);
      Assert.Equal(dataModels[i].MinRelationship, domainModels[i].MinRelationship);
      Assert.Equal(dataModels[i].MaxRelationship, domainModels[i].MaxRelationship);
      Assert.Equal(dataModels[i].HasTrait, domainModels[i].HasTrait);
      Assert.Equal(dataModels[i].SpokenText, domainModels[i].SpokenText);
    }
  }

  [Fact]
  public void MapToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    int? minRel = 5;
    int? maxRel = 95;
    Guid? hasTrait = Guid.NewGuid();
    const string spokenText = "Welcome, traveler.";

    var domain = Greeting.Load(id, minRel, maxRel, hasTrait, spokenText);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    Assert.Equal(id, dataModel.Id);
    Assert.Equal(minRel, dataModel.MinRelationship);
    Assert.Equal(maxRel, dataModel.MaxRelationship);
    Assert.Equal(hasTrait, dataModel.HasTrait);
    Assert.Equal(spokenText, dataModel.SpokenText);
  }

  #endregion
}
