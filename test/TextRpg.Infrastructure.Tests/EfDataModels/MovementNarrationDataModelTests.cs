using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class MovementNarrationDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var movementId = Guid.NewGuid();
    var text = "You walk into the street.";

    // Act
    var narration = new MovementNarrationDataModel
    {
      Id = id,
      MovementId = movementId,
      Text = text
    };

    // Assert
    Assert.Equal(id, narration.Id);
    Assert.Equal(movementId, narration.MovementId);
    Assert.Equal(text, narration.Text);
  }

  #endregion
}
