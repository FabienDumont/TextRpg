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
    Assert.NotNull(location);
    Assert.NotEqual(Guid.Empty, location.Id);
    Assert.Equal(string.Empty, location.Name);
    Assert.True(location.IsAlwaysOpen);
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
    Assert.NotNull(location);
    Assert.Equal(id, location.Id);
    Assert.Equal(name, location.Name);
    Assert.True(location.IsAlwaysOpen);
  }

  #endregion
}
