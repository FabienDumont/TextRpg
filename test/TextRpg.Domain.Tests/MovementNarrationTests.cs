namespace TextRpg.Domain.Tests;

public class MovementNarrationNarrationTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeWithGeneratedId()
  {
    // Arrange
    var movementId = Guid.NewGuid();
    var text = string.Empty;

    // Act
    var movement = MovementNarration.Create(movementId, text);

    // Assert
    movement.Should().NotBeNull();
    movement.Id.Should().NotBe(Guid.Empty);
    movement.MovementId.Should().Be(movementId);
    movement.Text.Should().Be(text);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var movementId = Guid.NewGuid();
    var text = string.Empty;

    // Act
    var movement = MovementNarration.Load(id, movementId, text);

    // Assert
    movement.Should().NotBeNull();
    movement.Id.Should().Be(id);
    movement.MovementId.Should().Be(movementId);
    movement.Text.Should().Be(text);
  }

  #endregion
}
