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
    const bool isAlwaysOpen = true;

    // Act
    var location = new LocationDataModel
    {
      Id = id,
      Name = name,
      IsAlwaysOpen = isAlwaysOpen
    };

    // Assert
    location.Id.Should().Be(id);
    location.Name.Should().Be(name);
    location.IsAlwaysOpen.Should().Be(isAlwaysOpen);
  }

  #endregion
}
