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
    Assert.Equal(id, movement.Id);
    Assert.Equal(fromLocationId, movement.FromLocationId);
    Assert.Equal(fromRoomId, movement.FromRoomId);
    Assert.Equal(toLocationId, movement.ToLocationId);
    Assert.Equal(toRoomId, movement.ToRoomId);
    Assert.Equal(requiredItemId, movement.RequiredItemId);
  }

  #endregion
}
