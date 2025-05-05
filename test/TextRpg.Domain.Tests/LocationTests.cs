namespace TextRpg.Domain.Tests;

public class LocationTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitialize()
  {
    // Act
    var location = Location.Create(string.Empty);

    // Assert
    location.Should().NotBeNull();
    location.Id.Should().NotBe(Guid.Empty);
    location.Name.Should().Be(string.Empty);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "Home";

    // Act
    var location = Location.Load(id, name);

    // Assert
    location.Id.Should().Be(id);
    location.Name.Should().Be(name);
  }

  #endregion
}
