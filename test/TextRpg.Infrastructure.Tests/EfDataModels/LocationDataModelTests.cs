using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class LocationDataModelTests
{
  #region Methods

  [Fact]
  public void Instanciation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    const string name = "Home";
    var rooms = new List<RoomDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        Name = "Test Room",
        LocationId = id,
        IsPlayerSpawn = true
      }
    };

    // Act
    var location = new LocationDataModel
    {
      Id = id,
      Name = name,
      Rooms = rooms
    };

    // Assert
    location.Id.Should().Be(id);
    location.Name.Should().Be(name);
    location.Rooms.Should().BeEquivalentTo(rooms);
  }

  #endregion
}
