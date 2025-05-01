using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class MovementNarrationServiceTests
{
  #region Fields

  private readonly IMovementNarrationRepository _repository = A.Fake<IMovementNarrationRepository>();
  private readonly MovementNarrationService _service;

  #endregion

  #region Ctors

  #region Constructor

  public MovementNarrationServiceTests()
  {
    _service = new MovementNarrationService(_repository);
  }

  #endregion

  #endregion

  #region Tests

  [Fact]
  public async Task GetNarrationTextAsync_ShouldReturnText_WhenNarrationExists()
  {
    // Arrange
    var movementId = Guid.NewGuid();
    const string expectedText = "You enter the bedroom.";
    var narration = MovementNarration.Load(Guid.NewGuid(), movementId, expectedText);

    A.CallTo(() => _repository.GetMovementNarrationFromMovementIdAsync(movementId, A<CancellationToken>._))
      .Returns(Task.FromResult(narration));

    // Act
    var result = await _service.GetNarrationTextAsync(movementId, CancellationToken.None);

    // Assert
    result.Should().Be(expectedText);
  }

  [Fact]
  public async Task GetNarrationTextAsync_ShouldThrow_WhenRepositoryThrows()
  {
    // Arrange
    var movementId = Guid.NewGuid();

    A.CallTo(() => _repository.GetMovementNarrationFromMovementIdAsync(movementId, A<CancellationToken>._))
      .Throws(new InvalidOperationException("Narration not found"));

    // Act
    Func<Task> act = async () => await _service.GetNarrationTextAsync(movementId, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Narration not found");
  }

  #endregion
}
