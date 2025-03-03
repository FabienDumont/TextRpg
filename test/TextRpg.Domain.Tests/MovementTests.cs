namespace TextRpg.Domain.Tests;

public class MovementTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeWithGeneratedId()
  {
    // Arrange
    var fromLocationId = Guid.NewGuid();
    var fromRoomId = Guid.NewGuid();
    var toLocationId = Guid.NewGuid();
    var toRoomId = Guid.NewGuid();
    var requiredItemId = Guid.NewGuid();

    // Act
    var movement = Movement.Create(fromLocationId, fromRoomId, toLocationId, toRoomId, requiredItemId);

    // Assert
    movement.Should().NotBeNull();
    movement.Id.Should().NotBe(Guid.Empty);
    movement.FromLocationId.Should().Be(fromLocationId);
    movement.FromRoomId.Should().Be(fromRoomId);
    movement.ToLocationId.Should().Be(toLocationId);
    movement.ToRoomId.Should().Be(toRoomId);
    movement.RequiredItemId.Should().Be(requiredItemId);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var fromLocationId = Guid.NewGuid();
    var fromRoomId = Guid.NewGuid();
    var toLocationId = Guid.NewGuid();
    var toRoomId = Guid.NewGuid();
    var requiredItemId = Guid.NewGuid();

    // Act
    var movement = Movement.Load(id, fromLocationId, fromRoomId, toLocationId, toRoomId, requiredItemId);

    // Assert
    movement.Should().NotBeNull();
    movement.Id.Should().Be(id);
    movement.FromLocationId.Should().Be(fromLocationId);
    movement.FromRoomId.Should().Be(fromRoomId);
    movement.ToLocationId.Should().Be(toLocationId);
    movement.ToRoomId.Should().Be(toRoomId);
    movement.RequiredItemId.Should().Be(requiredItemId);
  }

  #endregion
}
