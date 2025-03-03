using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class ExplorationActionResultNarrationDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var narrationId = Guid.NewGuid();
    var resultId = Guid.NewGuid();
    const string text = "You are too tired to think straight.";

    var result = new ExplorationActionResultDataModel
    {
      Id = resultId,
      ExplorationActionId = Guid.NewGuid(),
      AddMinutes = true
    };

    // Act
    var narration = new ExplorationActionResultNarrationDataModel
    {
      Id = narrationId,
      ExplorationActionResultId = resultId,
      MinEnergy = 0,
      MaxEnergy = 25,
      Text = text,
      ExplorationActionResult = result
    };

    // Assert
    Assert.Equal(narrationId, narration.Id);
    Assert.Equal(resultId, narration.ExplorationActionResultId);
    Assert.Equal(0, narration.MinEnergy);
    Assert.Equal(25, narration.MaxEnergy);
    Assert.Equal(text, narration.Text);
    Assert.Equal(result, narration.ExplorationActionResult);
  }

  [Fact]
  public void Instantiation_ShouldAllowNullBoundsAndResult()
  {
    // Arrange
    var narration = new ExplorationActionResultNarrationDataModel
    {
      Id = Guid.NewGuid(),
      ExplorationActionResultId = Guid.NewGuid(),
      MinEnergy = null,
      MaxEnergy = null,
      Text = "You lie down and let your thoughts wander.",
      ExplorationActionResult = null
    };

    // Act & Assert
    Assert.Null(narration.MinEnergy);
    Assert.Null(narration.MaxEnergy);
    Assert.Null(narration.ExplorationActionResult);
  }

  #endregion
}
