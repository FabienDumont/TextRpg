using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class RoomDataModelTests
{
  #region Methods

  [Fact]
  public void Instanciation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var roomId = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    const string roomName = "Bedroom";
    const string locationName = "Home";

    var location = new LocationDataModel
    {
      Id = locationId,
      Name = locationName
    };

    // Act
    var room = new RoomDataModel
    {
      Id = roomId,
      LocationId = locationId,
      Name = roomName,
      IsPlayerSpawn = false,
      Location = location
    };

    // Assert
    room.Id.Should().Be(roomId);
    room.Name.Should().Be(roomName);
    room.LocationId.Should().Be(locationId);
    room.IsPlayerSpawn.Should().Be(false);
    room.Location.Should().NotBeNull();
    room.Location.Id.Should().Be(locationId);
    room.Location.Name.Should().Be(locationName);
  }

  #endregion
}
