namespace TextRpg.Domain.Tests;

public class ExplorationActionResultNarrationTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeWithGeneratedId()
  {
    // Arrange
    var resultId = Guid.NewGuid();
    const int minEnergy = 10;
    const int maxEnergy = 50;
    const string text = "You lie down and close your eyes.";

    // Act
    var narration = ExplorationActionResultNarration.Create(resultId, minEnergy, maxEnergy, text);

    // Assert
    Assert.NotNull(narration);
    Assert.NotEqual(Guid.Empty, narration.Id);
    Assert.Equal(resultId, narration.ExplorationActionResultId);
    Assert.Equal(minEnergy, narration.MinEnergy);
    Assert.Equal(maxEnergy, narration.MaxEnergy);
    Assert.Equal(text, narration.Text);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var resultId = Guid.NewGuid();
    const int minEnergy = 5;
    const int maxEnergy = 20;
    const string text = "You collapse onto the bed.";

    // Act
    var narration = ExplorationActionResultNarration.Load(id, resultId, minEnergy, maxEnergy, text);

    // Assert
    Assert.NotNull(narration);
    Assert.Equal(id, narration.Id);
    Assert.Equal(resultId, narration.ExplorationActionResultId);
    Assert.Equal(minEnergy, narration.MinEnergy);
    Assert.Equal(maxEnergy, narration.MaxEnergy);
    Assert.Equal(text, narration.Text);
  }

  [Fact]
  public void Create_ShouldAllowNullBounds()
  {
    // Arrange
    var resultId = Guid.NewGuid();
    const string text = "You rest your head for a moment.";

    // Act
    var narration = ExplorationActionResultNarration.Create(resultId, null, null, text);

    // Assert
    Assert.Null(narration.MinEnergy);
    Assert.Null(narration.MaxEnergy);
    Assert.Equal(text, narration.Text);
  }

  #endregion
}
