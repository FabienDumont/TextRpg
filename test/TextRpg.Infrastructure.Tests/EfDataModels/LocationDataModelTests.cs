using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class LocationDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithGivenValues()
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
    Assert.Equal(id, location.Id);
    Assert.Equal(name, location.Name);
    Assert.Equal(isAlwaysOpen, location.IsAlwaysOpen);
  }

  #endregion
}
