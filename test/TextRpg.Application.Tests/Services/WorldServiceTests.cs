using TextRpg.Application.Services;
using TextRpg.Domain;
using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Application.Tests.Services;

public class WorldServiceTests
{
  #region Fields

  private readonly ICharacterService _characterService;
  private readonly ILocationService _locationService;
  private readonly IRoomService _roomService;

  private readonly WorldService _worldService;

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

    A.CallTo(() => _roomService.GetPlayerSpawnAsync(CancellationToken.None)).Returns(room);
    A.CallTo(() => _locationService.GetByIdAsync(location.Id, CancellationToken.None)).Returns(location);

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
  public async Task CreateNewWorld_ShouldThrow_WhenSpawnRoomIsNull()
  {
    // Arrange
    var date = new DateTime(2025, 4, 24);
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var gameSettings = GameSettings.Create(10);

    A.CallTo(() => _roomService.GetPlayerSpawnAsync(CancellationToken.None)).Returns((Room?) null);

    // Act
    var act = async () =>
      await _worldService.CreateNewWorldAsync(date, playerCharacter, gameSettings, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("No spawn room found.");
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
