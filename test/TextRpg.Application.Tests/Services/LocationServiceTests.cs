using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class LocationServiceTests
{
  #region Fields

  private readonly ILocationRepository _repository = A.Fake<ILocationRepository>();
  private readonly LocationService _service;

  #endregion

  #region Ctors

  public LocationServiceTests()
  {
    _service = new LocationService(_repository);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task GetByIdAsync_ShouldReturnLocation_WhenLocationExists()
  {
    // Arrange
    var expected = Location.Load(Guid.NewGuid(), "Market");
    A.CallTo(() => _repository.GetByIdAsync(expected.Id, A<CancellationToken>._)).Returns(Task.FromResult(expected));

    // Act
    var result = await _service.GetByIdAsync(expected.Id, CancellationToken.None);

    // Assert
    result.Should().Be(expected);
  }

  [Fact]
  public async Task GetByIdAsync_ShouldThrow_WhenRepositoryThrows()
  {
    // Arrange
    var randomId = Guid.NewGuid();
    A.CallTo(() => _repository.GetByIdAsync(randomId, A<CancellationToken>._)).Throws(
      new InvalidOperationException($"Location with ID {randomId} was not found.")
    );

    // Act
    Func<Task> act = async () => await _service.GetByIdAsync(randomId, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<InvalidOperationException>()
      .WithMessage($"Location with ID {randomId} was not found.");
  }

  #endregion
}
