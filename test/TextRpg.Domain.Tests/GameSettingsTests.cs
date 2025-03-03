namespace TextRpg.Domain.Tests;

public class GameSettingsTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeCorrectly()
  {
    // Arrange

    // Act
    var savePreferencesSet = GameSettings.Create(10);

    // Assert
    savePreferencesSet.Should().NotBeNull();
    savePreferencesSet.Id.Should().NotBe(Guid.Empty);
    savePreferencesSet.RandomNpcCount.Should().Be(10);
  }

  [Fact]
  public void Load_ShouldInitializeCorrectly()
  {
    // Arrange
    var id = Guid.NewGuid();

    // Act
    var save = GameSettings.Load(id, 10);

    // Assert
    save.Id.Should().Be(id);
    save.RandomNpcCount.Should().Be(10);
  }

  #endregion
}
