using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class SaveServiceTests
{
  [Fact]
  public async Task SaveGameAsync_Should_Call_Repository_With_Correct_Arguments()
  {
    // Arrange
    var repo = A.Fake<IGameSaveRepository>();
    var service = new SaveService(repo);
    var player = Character.Create("Player", 18, BiologicalSex.Male);
    var save = GameSave.Create("TestSave", player, World.Create(DateTime.Now, [player]));

    // Act
    await service.SaveGameAsync(save);

    // Assert
    A.CallTo(() => repo.SaveAsync(save, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void LoadGame_Should_Return_GameSave_From_Repository()
  {
    // Arrange
    var repo = A.Fake<IGameSaveRepository>();
    var json = "{ valid json }";
    var player = Character.Create("PlayerName", 18, BiologicalSex.Male);
    var expectedSave = GameSave.Create("TestSave", player, World.Create(DateTime.Now, [player]));

    A.CallTo(() => repo.Load(json)).Returns(expectedSave);

    var service = new SaveService(repo);

    // Act
    var result = service.LoadGame(json);

    // Assert
    result.Should().BeSameAs(expectedSave);
    A.CallTo(() => repo.Load(json)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void LoadGame_Should_Return_Null_When_Repo_Returns_Null()
  {
    // Arrange
    var repo = A.Fake<IGameSaveRepository>();
    A.CallTo(() => repo.Load("NotJson")).Returns(null);

    var service = new SaveService(repo);

    // Act
    var result = service.LoadGame("NotJson");

    // Assert
    result.Should().BeNull();
    A.CallTo(() => repo.Load("NotJson")).MustHaveHappenedOnceExactly();
  }
}
