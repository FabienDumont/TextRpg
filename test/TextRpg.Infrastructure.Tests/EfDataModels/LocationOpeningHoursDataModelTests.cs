using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class LocationOpeningHoursDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var roomId = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    var opensAt = new TimeSpan(8, 0, 0);
    var closesAt = new TimeSpan(10, 0, 0);

    var location = new LocationDataModel
    {
      Id = locationId,
      Name = "Home",
      IsAlwaysOpen = true
    };

    // Act
    var openingHours = new LocationOpeningHoursDataModel
    {
      Id = roomId,
      LocationId = locationId,
      DayOfWeek = DayOfWeek.Monday,
      OpensAt = opensAt,
      ClosesAt = closesAt,
      Location = location
    };

    // Assert
    openingHours.Id.Should().Be(roomId);
    openingHours.LocationId.Should().Be(locationId);
    openingHours.OpensAt.Should().Be(opensAt);
    openingHours.ClosesAt.Should().Be(closesAt);
    openingHours.Location.Should().Be(location);
  }

  #endregion
}
