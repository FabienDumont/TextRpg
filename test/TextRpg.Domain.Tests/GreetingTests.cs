namespace TextRpg.Domain.Tests;

public class GreetingTests
{
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
    greeting.Should().NotBeNull();
    greeting.Id.Should().NotBe(Guid.Empty);
    greeting.MinRelationship.Should().Be(minRelationship);
    greeting.MaxRelationship.Should().Be(maxRelationship);
    greeting.HasTrait.Should().Be(hasTrait);
    greeting.SpokenText.Should().Be(spokenText);
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
    greeting.Id.Should().Be(id);
    greeting.MinRelationship.Should().Be(minRelationship);
    greeting.MaxRelationship.Should().Be(maxRelationship);
    greeting.HasTrait.Should().Be(hasTrait);
    greeting.SpokenText.Should().Be(spokenText);
  }

}
