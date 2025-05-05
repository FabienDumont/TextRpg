namespace TextRpg.Domain.Tests;

public class RoomTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitialize()
  {
    // Act
    var room = Room.Create(Guid.Empty, string.Empty, true);

    // Assert
    room.Should().NotBeNull();
    room.Id.Should().NotBe(Guid.Empty);
    room.LocationId.Should().Be(Guid.Empty);
    room.Name.Should().Be(string.Empty);
    room.IsPlayerSpawn.Should().Be(true);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    const string name = "Home";

    // Act
    var room = Room.Load(id, locationId, name, true);

    // Assert
    room.Id.Should().Be(id);
    room.LocationId.Should().Be(locationId);
    room.Name.Should().Be(name);
    room.IsPlayerSpawn.Should().Be(true);
  }

  #endregion
}
