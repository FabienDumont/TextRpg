using FluentAssertions;
using Xunit;

namespace TextRpg.Domain.Tests;

public class ExplorationActionTests
{
  #region Methods

  [Fact]
  public void Create_ShouldInitializeWithGeneratedId()
  {
    // Arrange
    var locationId = Guid.NewGuid();
    var label = "Search the Cave";

    // Act
    var action = ExplorationAction.Create(locationId, label);

    // Assert
    action.Should().NotBeNull();
    action.Id.Should().NotBe(Guid.Empty);
    action.LocationId.Should().Be(locationId);
    action.Label.Should().Be(label);
  }

  [Fact]
  public void Load_ShouldInitializeWithGivenValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    var label = "Talk to the Merchant";

    // Act
    var action = ExplorationAction.Load(id, locationId, label);

    // Assert
    action.Should().NotBeNull();
    action.Id.Should().Be(id);
    action.LocationId.Should().Be(locationId);
    action.Label.Should().Be(label);
  }

  #endregion
}
