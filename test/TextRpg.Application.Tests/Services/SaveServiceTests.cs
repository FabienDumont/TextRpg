using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;
using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Application.Tests.Services;

public class SaveServiceTests
{
  #region Fields

  private readonly IGameSaveRepository _repository = A.Fake<IGameSaveRepository>();
  private readonly IWorldService _worldService = A.Fake<IWorldService>();
  private readonly SaveService _saveService;

  #endregion

  #region Ctors

  public SaveServiceTests()
  {
    _saveService = new SaveService(_repository, _worldService);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task SaveGameAsync_Should_Call_Repository_With_Correct_Arguments()
  {
    // Arrange
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var save = GameSave.Create("TestSave", playerCharacter, World.Create(DateTime.Now, [playerCharacter]));

    // Act
    await _saveService.SaveGameAsync(save);

    // Assert
    A.CallTo(() => _repository.SaveAsync(save, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void LoadGame_Should_Return_GameSave_From_Repository()
  {
    // Arrange
    var json = "{ valid json }";
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var expectedSave = GameSave.Create("TestSave", playerCharacter, World.Create(DateTime.Now, [playerCharacter]));

    A.CallTo(() => _repository.Load(json)).Returns(expectedSave);

    // Act
    var result = _saveService.LoadGame(json);

    // Assert
    result.Should().BeSameAs(expectedSave);
    A.CallTo(() => _repository.Load(json)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void LoadGame_Should_Return_Null_When_Repo_Returns_Null()
  {
    // Arrange
    A.CallTo(() => _repository.Load("NotJson")).Returns(null);

    // Act
    var result = _saveService.LoadGame("NotJson");

    // Assert
    result.Should().BeNull();
    A.CallTo(() => _repository.Load("NotJson")).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public async Task CreateNewSave_ShouldCreateNewSave_WithCorrectValues()
  {
    // Arrange
    var date = new DateTime(2025, 4, 24, 14, 30, 0);
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    var world = World.Create(date, [playerCharacter]);
    var gameSettings = GameSettings.Create(10);

    A.CallTo(() => _worldService.CreateNewWorldAsync(date, playerCharacter, gameSettings, CancellationToken.None))
      .Returns(world);

    // Act
    var save = await _saveService.CreateNewSaveAsync(date, playerCharacter, gameSettings, CancellationToken.None);

    // Assert
    save.Should().NotBeNull();
    save.PlayerCharacter.Should().Be(playerCharacter);
    save.World.Should().Be(world);
    save.Name.Should().Be($"{playerCharacter.Name}_{world.CurrentDate:yyyyMMdd_HHmmss}");

    A.CallTo(() => _worldService.CreateNewWorldAsync(date, playerCharacter, gameSettings, CancellationToken.None))
      .MustHaveHappenedOnceExactly();
  }

  #endregion
}
