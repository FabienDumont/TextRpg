using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class MovementServiceTests
{
  #region Fields

  private readonly IMovementRepository _repository = A.Fake<IMovementRepository>();
  private readonly MovementService _service;

  #endregion

  #region Ctors

  public MovementServiceTests()
  {
    _service = new MovementService(_repository);
  }

  #endregion

  #region Tests

  [Fact]
  public async Task GetAvailableMovementsAsync_ShouldReturnMovements_WhenMovementsExist()
  {
    // Arrange
    var currentLocationId = Guid.NewGuid();
    var currentRoomId = Guid.NewGuid();
    var expectedMovements = new List<Movement>
    {
      Movement.Load(Guid.NewGuid(), currentLocationId, currentRoomId, Guid.NewGuid(), Guid.NewGuid(), null),
      Movement.Load(Guid.NewGuid(), currentLocationId, currentRoomId, Guid.NewGuid(), null, null)
    };

    A.CallTo(() => _repository.GetAvailableMovementsAsync(currentLocationId, currentRoomId, A<CancellationToken>._))
      .Returns(Task.FromResult(expectedMovements));

    // Act
    var result = await _service.GetAvailableMovementsAsync(currentLocationId, currentRoomId, CancellationToken.None);

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

    A.CallTo(() => _repository.GetAvailableMovementsAsync(currentLocationId, currentRoomId, A<CancellationToken>._))
      .Throws(new InvalidOperationException("Failed to get movements"));

    // Act
    Func<Task> act = async () =>
      await _service.GetAvailableMovementsAsync(currentLocationId, currentRoomId, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Failed to get movements");
  }

  #endregion
}
