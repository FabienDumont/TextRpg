using MockQueryable.FakeItEasy;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class ExplorationActionRepositoryTests
{
  #region Fields

  private readonly Guid _locationId = Guid.NewGuid();
  private readonly ExplorationActionRepository _repository;
  private readonly Guid _roomId = Guid.NewGuid();

  #endregion

  #region Ctors

  public ExplorationActionRepositoryTests()
  {
    var data = new List<ExplorationActionDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        LocationId = _locationId,
        RoomId = _roomId,
        Label = "Sleep",
        NeededMinutes = 480
      },
      new()
      {
        Id = Guid.NewGuid(),
        LocationId = _locationId,
        RoomId = null,
        Label = "Nap",
        NeededMinutes = 60
      },
      new()
      {
        Id = Guid.NewGuid(),
        LocationId = Guid.NewGuid(),
        RoomId = _roomId,
        Label = "Wait",
        NeededMinutes = 15
      }
    };

    var context = A.Fake<ApplicationContext>();
    var mockDbSet = data.AsQueryable().BuildMockDbSet();
    A.CallTo(() => context.ExplorationActions).Returns(mockDbSet);

    _repository = new ExplorationActionRepository(context);
  }

  #endregion

  #region Tests

  [Fact]
  public async Task GetByLocationAndRoomIdAsync_ShouldReturnMatchingWithRoomId()
  {
    // Act
    var result = await _repository.GetByLocationAndRoomIdAsync(_locationId, _roomId, CancellationToken.None);

    // Assert
    result.Should().HaveCount(1);
    result[0].RoomId.Should().Be(_roomId);
    result[0].LocationId.Should().Be(_locationId);
  }

  [Fact]
  public async Task GetByLocationAndRoomIdAsync_ShouldReturnMatchingWithoutRoomId()
  {
    // Act
    var result = await _repository.GetByLocationAndRoomIdAsync(_locationId, null, CancellationToken.None);

    // Assert
    result.Should().HaveCount(1);
    result[0].RoomId.Should().BeNull();
    result[0].LocationId.Should().Be(_locationId);
  }

  [Fact]
  public async Task GetByLocationAndRoomIdAsync_ShouldReturnEmpty_WhenNoMatch()
  {
    // Arrange
    var nonexistentLocation = Guid.NewGuid();

    // Act
    var result = await _repository.GetByLocationAndRoomIdAsync(nonexistentLocation, _roomId, CancellationToken.None);

    // Assert
    result.Should().BeEmpty();
  }

  #endregion
}
