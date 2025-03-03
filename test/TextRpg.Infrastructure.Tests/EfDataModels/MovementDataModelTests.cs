using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class MovementDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var fromLocationId = Guid.NewGuid();
    var fromRoomId = Guid.NewGuid();
    var toLocationId = Guid.NewGuid();
    var toRoomId = Guid.NewGuid();
    var requiredItemId = Guid.NewGuid();

    // Act
    var movement = new MovementDataModel
    {
      Id = id,
      FromLocationId = fromLocationId,
      FromRoomId = fromRoomId,
      ToLocationId = toLocationId,
      ToRoomId = toRoomId,
      RequiredItemId = requiredItemId
    };

    // Assert
    movement.Id.Should().Be(id);
    movement.FromLocationId.Should().Be(fromLocationId);
    movement.FromRoomId.Should().Be(fromRoomId);
    movement.ToLocationId.Should().Be(toLocationId);
    movement.ToRoomId.Should().Be(toRoomId);
    movement.RequiredItemId.Should().Be(requiredItemId);
  }

  #endregion
}
