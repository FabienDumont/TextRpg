using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class MovementServiceTests
{
  #region Fields

  private readonly IMovementRepository _repository = A.Fake<IMovementRepository>();
  private readonly MovementService _service;
  private readonly ILocationOpeningHoursService _locationOpeningHoursService = A.Fake<ILocationOpeningHoursService>();

  #endregion

  #region Ctors

  public MovementServiceTests()
  {
    _service = new MovementService(_repository, _locationOpeningHoursService);
  }

  #endregion

  #region Tests

  [Fact]
  public async Task GetAvailableMovementsAsync_ShouldReturnMovements_WhenMovementsExist()
  {
    // Arrange
    var currentLocationId = Guid.NewGuid();
    var currentRoomId = Guid.NewGuid();
    var destinationLocation1Id = Guid.NewGuid();
    var destinationLocation2Id = Guid.NewGuid();
    const DayOfWeek dayOfWeek = DayOfWeek.Monday;
    var timeOfDay = new TimeSpan(8, 0, 0);
    var expectedMovements = new List<Movement>
    {
      Movement.Load(Guid.NewGuid(), currentLocationId, currentRoomId, destinationLocation1Id, Guid.NewGuid(), null),
      Movement.Load(Guid.NewGuid(), currentLocationId, currentRoomId, destinationLocation2Id, null, null)
    };

    A.CallTo(() => _repository.GetMovementsAsync(currentLocationId, currentRoomId, A<CancellationToken>._))
      .Returns(Task.FromResult(expectedMovements));
    A.CallTo(() => _locationOpeningHoursService.IsLocationOpenAsync(
        destinationLocation1Id, dayOfWeek, timeOfDay, CancellationToken.None
      )
    ).Returns(true);
    A.CallTo(() => _locationOpeningHoursService.IsLocationOpenAsync(
        destinationLocation2Id, dayOfWeek, timeOfDay, CancellationToken.None
      )
    ).Returns(true);

    // Act
    var result = await _service.GetAvailableMovementsAsync(
      currentLocationId, currentRoomId, dayOfWeek, timeOfDay, CancellationToken.None
    );

    // Assert
    result.Should().NotBeNull();
    result.Should().BeEquivalentTo(expectedMovements);
  }

  [Fact]
  public async Task GetAvailableMovementsAsync_ShouldThrow_WhenRepositoryThrows()
  {
    // Arrange
    var currentLocationId = Guid.NewGuid();
    var currentRoomId = Guid.NewGuid();
    var dayOfWeek = DayOfWeek.Monday;
    var timeOfDay = new TimeSpan(8, 0, 0);

    A.CallTo(() => _repository.GetMovementsAsync(currentLocationId, currentRoomId, A<CancellationToken>._))
      .Throws(new InvalidOperationException("Failed to get movements"));

    // Act
    Func<Task> act = async () => await _service.GetAvailableMovementsAsync(
      currentLocationId, currentRoomId, dayOfWeek, timeOfDay, CancellationToken.None
    );

    // Assert
    await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Failed to get movements");
  }

  #endregion
}
