using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class MovementNarrationDataModelTests
{
  #region Methods

  #region Tests

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
    narration.Id.Should().Be(id);
    narration.MovementId.Should().Be(movementId);
    narration.Text.Should().Be(text);
  }

  #endregion

  #endregion
}
