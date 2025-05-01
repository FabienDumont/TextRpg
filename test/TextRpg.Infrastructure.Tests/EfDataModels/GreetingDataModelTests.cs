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
    greeting.Id.Should().Be(id);
    greeting.MinRelationship.Should().Be(minRelationship);
    greeting.MaxRelationship.Should().Be(maxRelationship);
    greeting.HasTrait.Should().Be(hasTrait);
    greeting.SpokenText.Should().Be(spokenText);
  }

  #endregion
}
