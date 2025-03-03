using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class LocationOpeningHoursServiceTests
{
  #region Fields

  private readonly ILocationOpeningHoursRepository _repository = A.Fake<ILocationOpeningHoursRepository>();
  private readonly LocationOpeningHoursService _service;

  #endregion

  #region Ctors

  public LocationOpeningHoursServiceTests()
  {
    _service = new LocationOpeningHoursService(_repository);
  }

  #endregion

  #region Tests

  [Fact]
  public async Task IsLocationOpenAsync_ShouldReturnTrue_WhenWithinNormalHours()
  {
    var locationId = Guid.NewGuid();
    const DayOfWeek day = DayOfWeek.Monday;
    var opensAt = new TimeSpan(9, 0, 0);
    var closesAt = new TimeSpan(17, 0, 0);
    var testTime = new TimeSpan(10, 0, 0);

    var openingHours = new List<LocationOpeningHours>
    {
      LocationOpeningHours.Create(locationId, day, opensAt, closesAt)
    };

    A.CallTo(() => _repository.GetByLocationIdAsync(locationId, A<CancellationToken>._)).Returns(openingHours);

    var result = await _service.IsLocationOpenAsync(locationId, day, testTime, CancellationToken.None);

    Assert.True(result);
  }

  [Fact]
  public async Task IsLocationOpenAsync_ShouldReturnFalse_WhenOutsideNormalHours()
  {
    var locationId = Guid.NewGuid();
    const DayOfWeek day = DayOfWeek.Monday;
    var opensAt = new TimeSpan(9, 0, 0);
    var closesAt = new TimeSpan(17, 0, 0);
    var testTime = new TimeSpan(18, 0, 0);

    var openingHours = new List<LocationOpeningHours>
    {
      LocationOpeningHours.Create(locationId, day, opensAt, closesAt)
    };

    A.CallTo(() => _repository.GetByLocationIdAsync(locationId, A<CancellationToken>._)).Returns(openingHours);

    var result = await _service.IsLocationOpenAsync(locationId, day, testTime, CancellationToken.None);

    Assert.False(result);
  }

  [Fact]
  public async Task IsLocationOpenAsync_ShouldReturnTrue_WhenWithinCrossMidnightHours_AndNextDay()
  {
    var locationId = Guid.NewGuid();
    const DayOfWeek saturday = DayOfWeek.Saturday;
    const DayOfWeek sunday = DayOfWeek.Sunday;
    var opensAt = new TimeSpan(22, 0, 0);
    var closesAt = new TimeSpan(2, 0, 0);
    var testTime = new TimeSpan(1, 0, 0);

    var openingHours = new List<LocationOpeningHours>
    {
      LocationOpeningHours.Create(locationId, saturday, opensAt, closesAt)
    };

    A.CallTo(() => _repository.GetByLocationIdAsync(locationId, A<CancellationToken>._)).Returns(openingHours);

    var result = await _service.IsLocationOpenAsync(locationId, sunday, testTime, CancellationToken.None);

    Assert.True(result);
  }

  [Fact]
  public async Task IsLocationOpenAsync_ShouldReturnFalse_WhenNoOpeningHoursExist()
  {
    var locationId = Guid.NewGuid();
    const DayOfWeek day = DayOfWeek.Wednesday;
    var testTime = new TimeSpan(12, 0, 0);

    A.CallTo(() => _repository.GetByLocationIdAsync(locationId, A<CancellationToken>._)).Returns([]);

    var result = await _service.IsLocationOpenAsync(locationId, day, testTime, CancellationToken.None);

    Assert.False(result);
  }

  [Fact]
  public async Task IsLocationOpenAsync_ShouldReturnTrue_WhenWithinCrossMidnightHours_OnSameDay()
  {
    var locationId = Guid.NewGuid();
    const DayOfWeek day = DayOfWeek.Saturday;
    var opensAt = new TimeSpan(22, 0, 0);
    var closesAt = new TimeSpan(2, 0, 0);
    var testTime = new TimeSpan(23, 30, 0);

    var openingHours = new List<LocationOpeningHours>
    {
      LocationOpeningHours.Create(locationId, day, opensAt, closesAt)
    };

    A.CallTo(() => _repository.GetByLocationIdAsync(locationId, A<CancellationToken>._)).Returns(openingHours);

    var result = await _service.IsLocationOpenAsync(locationId, day, testTime, CancellationToken.None);

    Assert.True(result);
  }

  [Fact]
  public async Task IsLocationOpenAsync_ShouldReturnFalse_WhenDayDoesNotMatch_AndDoesNotCrossMidnight()
  {
    var locationId = Guid.NewGuid();
    const DayOfWeek actualDay = DayOfWeek.Tuesday;
    const DayOfWeek testDay = DayOfWeek.Monday;
    var opensAt = new TimeSpan(9, 0, 0);
    var closesAt = new TimeSpan(17, 0, 0);
    var testTime = new TimeSpan(10, 0, 0);

    var openingHours = new List<LocationOpeningHours>
    {
      LocationOpeningHours.Create(locationId, actualDay, opensAt, closesAt)
    };

    A.CallTo(() => _repository.GetByLocationIdAsync(locationId, A<CancellationToken>._)).Returns(openingHours);

    var result = await _service.IsLocationOpenAsync(locationId, testDay, testTime, CancellationToken.None);

    Assert.False(result);
  }

  [Fact]
  public async Task IsLocationOpenAsync_ShouldReturnFalse_WhenCrossMidnight_AndTimeOutsideClosingWindow()
  {
    var locationId = Guid.NewGuid();
    const DayOfWeek saturday = DayOfWeek.Saturday;
    const DayOfWeek sunday = DayOfWeek.Sunday;
    var opensAt = new TimeSpan(22, 0, 0);
    var closesAt = new TimeSpan(2, 0, 0);
    var testTime = new TimeSpan(5, 0, 0);

    var openingHours = new List<LocationOpeningHours>
    {
      LocationOpeningHours.Create(locationId, saturday, opensAt, closesAt)
    };

    A.CallTo(() => _repository.GetByLocationIdAsync(locationId, A<CancellationToken>._)).Returns(openingHours);

    var result = await _service.IsLocationOpenAsync(locationId, sunday, testTime, CancellationToken.None);

    Assert.False(result);
  }

  [Fact]
  public async Task IsLocationOpenAsync_ShouldReturnFalse_WhenCrossMidnight_OnSameDay_AndTimeBeforeOpening()
  {
    var locationId = Guid.NewGuid();
    const DayOfWeek day = DayOfWeek.Saturday;
    var opensAt = new TimeSpan(22, 0, 0);
    var closesAt = new TimeSpan(2, 0, 0);
    var testTime = new TimeSpan(21, 0, 0);

    var openingHours = new List<LocationOpeningHours>
    {
      LocationOpeningHours.Create(locationId, day, opensAt, closesAt)
    };

    A.CallTo(() => _repository.GetByLocationIdAsync(locationId, A<CancellationToken>._)).Returns(openingHours);

    var result = await _service.IsLocationOpenAsync(locationId, day, testTime, CancellationToken.None);

    Assert.False(result);
  }

  #endregion
}
