namespace TextRpg.Domain.Tests;

public class LocationTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitialize()
  {
    // Act
    var location = Location.Create(string.Empty, true);

    // Assert
    location.Should().NotBeNull();
    location.Id.Should().NotBe(Guid.Empty);
    location.Name.Should().Be(string.Empty);
    location.IsAlwaysOpen.Should().Be(true);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "Home";
    const bool isAlwaysOpen = true;

    // Act
    var location = Location.Load(id, name, isAlwaysOpen);

    // Assert
    location.Id.Should().Be(id);
    location.Name.Should().Be(name);
    location.IsAlwaysOpen.Should().Be(isAlwaysOpen);
  }

  #endregion
}
