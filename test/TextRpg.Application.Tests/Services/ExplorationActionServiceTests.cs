using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;
using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Application.Tests.Services;

public class ExplorationActionServiceTests
{
  #region Fields

  private readonly IExplorationActionResultNarrationService _explorationActionResultNarrationService =
    A.Fake<IExplorationActionResultNarrationService>();

  private readonly IExplorationActionResultService _explorationActionResultService =
    A.Fake<IExplorationActionResultService>();

  private readonly ILocationService _locationService = A.Fake<ILocationService>();
  private readonly IExplorationActionRepository _repository = A.Fake<IExplorationActionRepository>();
  private readonly ExplorationActionService _service;

  #endregion

  #region Ctors

  public ExplorationActionServiceTests()
  {
    _service = new ExplorationActionService(
      _repository, _locationService, _explorationActionResultService, _explorationActionResultNarrationService
    );
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

    Assert.Equal(actions, result);
  }

  [Fact]
  public async Task GetExplorationActionsAsync_ShouldFilterActions_WhenLocationHasLimitedHours()
  {
    var locationId = Guid.NewGuid();
    var roomId = Guid.NewGuid();
    var now = new TimeSpan(23, 50, 0);
    const DayOfWeek currentDay = DayOfWeek.Monday;

    var shortAction = ExplorationAction.Load(Guid.NewGuid(), locationId, roomId, "Short", 5);
    var longAction = ExplorationAction.Load(Guid.NewGuid(), locationId, roomId, "TooLong", 120);

    var actions = new List<ExplorationAction> {shortAction, longAction};

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

    Assert.Single(result);
    Assert.Equal("Short", result[0].Label);
  }

  [Fact]
  public async Task ExecuteAsync_ShouldApplyEffectsAndReturnNarration()
  {
    var actionId = Guid.NewGuid();
    var resultId = Guid.NewGuid();

    var action = ExplorationAction.Load(actionId, Guid.NewGuid(), Guid.NewGuid(), "Nap", 60);
    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.Energy = 50;
    character.Money = 10;

    var world = World.Create(DateTime.Now, [character]);
    var save = GameSave.Load(Guid.NewGuid(), "Save 1", character.Id, world, []);

    var result = ExplorationActionResult.Load(resultId, actionId, true, -10, 100);

    var narration = ExplorationActionResultNarration.Load(
      Guid.NewGuid(), resultId, "You feel strangely alert after the nap."
    );

    A.CallTo(() =>
      _explorationActionResultService.GetExplorationActionResultAsync(actionId, character, A<CancellationToken>._)
    ).Returns(result);
    A.CallTo(() =>
      _explorationActionResultNarrationService.GetExplorationActionResultNarrationAsync(
        resultId, character, A<CancellationToken>._
      )
    ).Returns(narration);

    var originalTime = save.World.CurrentDate;

    var returnedNarration = await _service.ExecuteAsync(action, save, CancellationToken.None);

    Assert.Equal(narration, returnedNarration);
    Assert.Equal(40, character.Energy);
    Assert.Equal(110, character.Money);
    Assert.Equal(originalTime.AddMinutes(60), save.World.CurrentDate);
  }

  #endregion
}
