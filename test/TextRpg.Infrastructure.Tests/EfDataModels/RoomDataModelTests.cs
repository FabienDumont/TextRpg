﻿using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class RoomDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var roomId = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    const string roomName = "Bedroom";

    var location = new LocationDataModel
    {
      Id = locationId,
      Name = "Home",
      IsAlwaysOpen = true
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
    room.Location.Should().Be(location);
  }

  #endregion
}
