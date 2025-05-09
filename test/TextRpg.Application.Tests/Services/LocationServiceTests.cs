using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class LocationServiceTests
{
  #region Fields

  private readonly ILocationOpeningHoursService _locationOpeningHoursService = A.Fake<ILocationOpeningHoursService>();

  private readonly ILocationRepository _repository = A.Fake<ILocationRepository>();
  private readonly LocationService _service;

  #endregion

  #region Ctors

  public LocationServiceTests()
  {
    _service = new LocationService(_repository, _locationOpeningHoursService);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task GetByIdAsync_ShouldReturnLocation_WhenLocationExists()
  {
    // Arrange
    var expected = Location.Load(Guid.NewGuid(), "Market", true);
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

  [Fact]
  public async Task IsLocationOpenAsync_ShouldReturnTrue_WhenLocationIsAlwaysOpen()
  {
    // Arrange
    var locationId = Guid.NewGuid();
    var location = Location.Load(locationId, "Library", true);
    var time = new TimeSpan(10, 0, 0);
    const DayOfWeek day = DayOfWeek.Tuesday;

    A.CallTo(() => _repository.GetByIdAsync(locationId, A<CancellationToken>._)).Returns(location);

    // Act
    var result = await _service.IsLocationOpenAsync(locationId, day, time, CancellationToken.None);

    // Assert
    result.Should().BeTrue();

    // Ensure opening hours service is NOT called
    A.CallTo(() => _locationOpeningHoursService.IsLocationOpenAsync(
        A<Guid>._, A<DayOfWeek>._, A<TimeSpan>._, A<CancellationToken>._
      )
    ).MustNotHaveHappened();
  }

  [Fact]
  public async Task IsLocationOpenAsync_ShouldDelegateToOpeningHoursService_WhenLocationIsNotAlwaysOpen()
  {
    // Arrange
    var locationId = Guid.NewGuid();
    var location = Location.Load(locationId, "College", false);
    var time = new TimeSpan(14, 0, 0);
    const DayOfWeek day = DayOfWeek.Monday;

    A.CallTo(() => _repository.GetByIdAsync(locationId, A<CancellationToken>._)).Returns(location);
    A.CallTo(() => _locationOpeningHoursService.IsLocationOpenAsync(locationId, day, time, A<CancellationToken>._))
      .Returns(true);

    // Act
    var result = await _service.IsLocationOpenAsync(locationId, day, time, CancellationToken.None);

    // Assert
    result.Should().BeTrue();

    A.CallTo(() => _locationOpeningHoursService.IsLocationOpenAsync(locationId, day, time, A<CancellationToken>._))
      .MustHaveHappenedOnceExactly();
  }

  #endregion
}
