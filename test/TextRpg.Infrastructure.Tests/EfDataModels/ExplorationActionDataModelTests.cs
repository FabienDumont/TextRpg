using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class ExplorationActionDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var actionId = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    var roomId = Guid.NewGuid();
    const string label = "Sleep";
    const int neededMinutes = 480;

    var location = new LocationDataModel
    {
      Id = locationId,
      Name = "Home",
      IsAlwaysOpen = false
    };

    // Act
    var action = new ExplorationActionDataModel
    {
      Id = actionId,
      LocationId = locationId,
      RoomId = roomId,
      Label = label,
      Location = location,
      NeededMinutes = neededMinutes
    };

    // Assert
    action.Id.Should().Be(actionId);
    action.LocationId.Should().Be(locationId);
    action.RoomId.Should().Be(roomId);
    action.Label.Should().Be(label);
    action.Location.Should().Be(location);
    action.NeededMinutes.Should().Be(neededMinutes);
  }

  [Fact]
  public void Instantiation_ShouldAllowNullRoomId()
  {
    // Arrange
    var action = new ExplorationActionDataModel
    {
      Id = Guid.NewGuid(),
      LocationId = Guid.NewGuid(),
      RoomId = null,
      Label = "Sleep",
      NeededMinutes = 480,
      Location = null
    };

    // Act & Assert
    action.RoomId.Should().BeNull();
    action.Location.Should().BeNull();
  }

  #endregion
}
