using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class ExplorationActionServiceTests
{
  #region Fields

  private readonly ILocationService _locationService = A.Fake<ILocationService>();
  private readonly IExplorationActionRepository _repository = A.Fake<IExplorationActionRepository>();
  private readonly ExplorationActionService _service;

  #endregion

  #region Ctors

  public ExplorationActionServiceTests()
  {
    _service = new ExplorationActionService(_repository, _locationService);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task GetExplorationActionsAsync_ShouldReturnAll_WhenLocationIsAlwaysOpen()
  {
    var locationId = Guid.NewGuid();
    var roomId = Guid.NewGuid();
    var actions = new List<ExplorationAction>
    {
      ExplorationAction.Load(Guid.NewGuid(), locationId, roomId, "Sleep", 60),
      ExplorationAction.Load(Guid.NewGuid(), locationId, roomId, "Rest", 10)
    };

    A.CallTo(() => _locationService.GetByIdAsync(locationId, A<CancellationToken>._))
      .Returns(Location.Load(locationId, "Home", true));
    A.CallTo(() => _repository.GetByLocationAndRoomIdAsync(locationId, roomId, A<CancellationToken>._))
      .Returns(actions);

    var result = await _service.GetExplorationActionsAsync(
      locationId, roomId, DayOfWeek.Monday, new TimeSpan(8, 0, 0), CancellationToken.None
    );

    result.Should().BeEquivalentTo(actions);
  }

  [Fact]
  public async Task GetExplorationActionsAsync_ShouldFilterActions_WhenLocationHasLimitedHours()
  {
    var locationId = Guid.NewGuid();
    var roomId = Guid.NewGuid();
    var now = new TimeSpan(23, 50, 0); // 11:50 PM
    const DayOfWeek currentDay = DayOfWeek.Monday;

    var actions = new List<ExplorationAction>
    {
      ExplorationAction.Load(Guid.NewGuid(), locationId, roomId, "Short", 5), // should pass
      ExplorationAction.Load(Guid.NewGuid(), locationId, roomId, "TooLong", 120) // should be filtered
    };

    A.CallTo(() => _locationService.GetByIdAsync(locationId, A<CancellationToken>._))
      .Returns(Location.Load(locationId, "Night Club", false));
    A.CallTo(() => _repository.GetByLocationAndRoomIdAsync(locationId, roomId, A<CancellationToken>._))
      .Returns(actions);

    A.CallTo(() => _locationService.IsLocationOpenAsync(
        locationId, currentDay, now.Add(TimeSpan.FromMinutes(5)), A<CancellationToken>._
      )
    ).Returns(true);
    A.CallTo(() => _locationService.IsLocationOpenAsync(
        locationId, DayOfWeek.Tuesday, now.Add(TimeSpan.FromMinutes(120)), A<CancellationToken>._
      )
    ).Returns(false);

    var result = await _service.GetExplorationActionsAsync(locationId, roomId, currentDay, now, CancellationToken.None);

    result.Should().ContainSingle(x => x.Label == "Short");
  }

  #endregion
}
