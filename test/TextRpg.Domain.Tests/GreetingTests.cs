namespace TextRpg.Domain.Tests;

public class GreetingTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitialize()
  {
    // Arrange
    int? minRelationship = 10;
    int? maxRelationship = 50;
    Guid? hasTrait = Guid.NewGuid();
    const string spokenText = "Hello there";

    // Act
    var greeting = Greeting.Create(minRelationship, maxRelationship, hasTrait, spokenText);

    // Assert
    Assert.NotNull(greeting);
    Assert.NotEqual(Guid.Empty, greeting.Id);
    Assert.Equal(minRelationship, greeting.MinRelationship);
    Assert.Equal(maxRelationship, greeting.MaxRelationship);
    Assert.Equal(hasTrait, greeting.HasTrait);
    Assert.Equal(spokenText, greeting.SpokenText);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    int? minRelationship = null;
    int? maxRelationship = 100;
    Guid? hasTrait = null;
    const string spokenText = "Hey!";

    // Act
    var greeting = Greeting.Load(id, minRelationship, maxRelationship, hasTrait, spokenText);

    // Assert
    Assert.Equal(id, greeting.Id);
    Assert.Equal(minRelationship, greeting.MinRelationship);
    Assert.Equal(maxRelationship, greeting.MaxRelationship);
    Assert.Equal(hasTrait, greeting.HasTrait);
    Assert.Equal(spokenText, greeting.SpokenText);
  }

  #endregion
}
