using TextRpg.Application.Services;
using TextRpg.Domain;
using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Application.Tests.Services;

public class WorldServiceTests
{
  #region Fields

  private readonly WorldService _worldService;
  private readonly ICharacterService _characterService;
  private readonly ILocationService _locationService;
  private readonly IRoomService _roomService;

  #endregion

  #region Ctors

  public WorldServiceTests()
  {
    _characterService = A.Fake<ICharacterService>();
    _locationService = A.Fake<ILocationService>();
    _roomService = A.Fake<IRoomService>();
    _worldService = new WorldService(_characterService, _locationService, _roomService);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task CreateNewWorld_ShouldIncludePlayerCharacter_AndAddTenRandomCharacters()
  {
    // Arrange
    var date = new DateTime(2025, 4, 24);
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var randomCharacters = new List<Character>();
    var gameSettings = GameSettings.Create(10);
    var location = Location.Create("Home", true);
    var room = Room.Create(location.Id, "Living room", true);

    A.CallTo(() => _locationService.GetPlayerSpawnAsync(CancellationToken.None)).Returns(location);
    A.CallTo(() => _roomService.GetLocationEntryPointAsync(location.Id, CancellationToken.None)).Returns(room);

    for (var i = 0; i < gameSettings.RandomNpcCount; i++)
    {
      var randomCharacter = CharacterHelper.GetRandomCharacter();
      randomCharacters.Add(randomCharacter);
      A.CallTo(() => _characterService.CreateRandomCharacterAsync())
        .ReturnsNextFromSequence(randomCharacters.ToArray());
    }

    // Act
    var world = await _worldService.CreateNewWorldAsync(date, playerCharacter, gameSettings, CancellationToken.None);

    // Assert
    world.Should().NotBeNull();
    world.Characters.Should().Contain(playerCharacter);
    world.Characters.Should().Contain(randomCharacters);
    world.Characters.Should().HaveCount(gameSettings.RandomNpcCount + 1);

    A.CallTo(() => _characterService.CreateRandomCharacterAsync())
      .MustHaveHappened(gameSettings.RandomNpcCount, Times.Exactly);
  }

  [Fact]
  public void AdvanceTime_ShouldAddMinutesToWorldCurrentDate()
  {
    // Arrange
    var initialDate = new DateTime(2025, 4, 24, 8, 0, 0);
    var world = World.Create(initialDate, []);

    const int minutesToAdvance = 90;

    // Act
    _worldService.AdvanceTime(world, minutesToAdvance);

    // Assert
    world.CurrentDate.Should().Be(initialDate.AddMinutes(minutesToAdvance));
  }

  #endregion
}
