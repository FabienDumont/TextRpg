namespace TextRpg.Domain.Tests;

public class ExplorationActionTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeWithGeneratedId()
  {
    // Arrange
    var locationId = Guid.NewGuid();
    var roomId = Guid.NewGuid();
    const string label = "Sleep";
    const int neededMinutes = 480;

    // Act
    var action = ExplorationAction.Create(locationId, roomId, label, 480);

    // Assert
    action.Should().NotBeNull();
    action.Id.Should().NotBe(Guid.Empty);
    action.LocationId.Should().Be(locationId);
    action.RoomId.Should().Be(roomId);
    action.Label.Should().Be(label);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    var roomId = Guid.NewGuid();
    const string label = "Sleep";
    const int neededMinutes = 480;

    // Act
    var action = ExplorationAction.Load(id, locationId, roomId, label, neededMinutes);

    // Assert
    action.Should().NotBeNull();
    action.Id.Should().Be(id);
    action.LocationId.Should().Be(locationId);
    action.RoomId.Should().Be(roomId);
    action.Label.Should().Be(label);
    action.NeededMinutes.Should().Be(neededMinutes);
  }

  #endregion
}
