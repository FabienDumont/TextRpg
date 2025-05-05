namespace TextRpg.Domain.Tests;

public class NarrationTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeWithGeneratedId()
  {
    // Arrange
    const string key = "intro_scene";
    const string text = "Welcome to the game.";

    // Act
    var narration = Narration.Create(key, text);

    // Assert
    narration.Should().NotBeNull();
    narration.Id.Should().NotBe(Guid.Empty);
    narration.Key.Should().Be(key);
    narration.Text.Should().Be(text);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string key = "intro_scene";
    const string text = "Welcome back.";

    // Act
    var narration = Narration.Load(id, key, text);

    // Assert
    narration.Should().NotBeNull();
    narration.Id.Should().Be(id);
    narration.Key.Should().Be(key);
    narration.Text.Should().Be(text);
  }

  #endregion
}
