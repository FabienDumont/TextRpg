namespace TextRpg.Domain.Tests;

public class GameSettingsTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeCorrectly()
  {
    // Act
    var settings = GameSettings.Create(10);

    // Assert
    Assert.NotNull(settings);
    Assert.NotEqual(Guid.Empty, settings.Id);
    Assert.Equal(10, settings.RandomNpcCount);
  }

  [Fact]
  public void Load_ShouldInitializeCorrectly()
  {
    // Arrange
    var id = Guid.NewGuid();

    // Act
    var settings = GameSettings.Load(id, 10);

    // Assert
    Assert.Equal(id, settings.Id);
    Assert.Equal(10, settings.RandomNpcCount);
  }

  #endregion
}
