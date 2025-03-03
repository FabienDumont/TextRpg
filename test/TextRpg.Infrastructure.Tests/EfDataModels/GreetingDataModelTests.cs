using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class GreetingDataModelTests
{
  #region Methods

  [Fact]
  public void Instanciation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    int? minRelationship = 10;
    int? maxRelationship = 80;
    var hasTrait = Guid.NewGuid();
    const string spokenText = "Hey there, stranger.";

    // Act
    var greeting = new GreetingDataModel
    {
      Id = id,
      MinRelationship = minRelationship,
      MaxRelationship = maxRelationship,
      HasTrait = hasTrait,
      SpokenText = spokenText
    };

    // Assert
    Assert.Equal(id, greeting.Id);
    Assert.Equal(minRelationship, greeting.MinRelationship);
    Assert.Equal(maxRelationship, greeting.MaxRelationship);
    Assert.Equal(hasTrait, greeting.HasTrait);
    Assert.Equal(spokenText, greeting.SpokenText);
  }

  #endregion
}
