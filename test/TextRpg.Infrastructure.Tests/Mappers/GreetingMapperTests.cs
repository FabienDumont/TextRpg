using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class GreetingMapperTests
{
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
    domain.Id.Should().Be(id);
    domain.MinRelationship.Should().Be(minRel);
    domain.MaxRelationship.Should().Be(maxRel);
    domain.HasTrait.Should().Be(hasTrait);
    domain.SpokenText.Should().Be(spokenText);
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
    domainModels.Should().HaveCount(2);

    for (int i = 0; i < domainModels.Count; i++)
    {
      domainModels[i].Id.Should().Be(dataModels[i].Id);
      domainModels[i].MinRelationship.Should().Be(dataModels[i].MinRelationship);
      domainModels[i].MaxRelationship.Should().Be(dataModels[i].MaxRelationship);
      domainModels[i].HasTrait.Should().Be(dataModels[i].HasTrait);
      domainModels[i].SpokenText.Should().Be(dataModels[i].SpokenText);
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
    dataModel.Id.Should().Be(id);
    dataModel.MinRelationship.Should().Be(minRel);
    dataModel.MaxRelationship.Should().Be(maxRel);
    dataModel.HasTrait.Should().Be(hasTrait);
    dataModel.SpokenText.Should().Be(spokenText);
  }
}
