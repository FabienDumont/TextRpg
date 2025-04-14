using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

using Xunit;
using FluentAssertions;
using FakeItEasy;
using System.Threading;
using System.Threading.Tasks;

public class SaveServiceTests
{
  [Fact]
  public async Task SaveGameAsync_Should_Call_Repository_With_Correct_Arguments()
  {
    // Arrange
    var repo = A.Fake<IGameSaveRepository>();
    var service = new SaveService(repo);
    var player = Character.Create("Player");
    var save = GameSave.Create("TestSave", player, World.Create(DateTime.Now, [player]));

    // Act
    await service.SaveGameAsync(save);

    // Assert
    A.CallTo(() => repo.SaveAsync(save, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public async Task LoadGameAsync_Should_Return_GameSave_From_Repository()
  {
    // Arrange
    var repo = A.Fake<IGameSaveRepository>();
    var player = Character.Create("Hero");
    var expectedSave = GameSave.Create("SaveSlot1", player, World.Create(DateTime.Now, [player]));
    A.CallTo(() => repo.LoadAsync("SaveSlot1", A<CancellationToken>._)).Returns(expectedSave);

    var service = new SaveService(repo);

    // Act
    var result = await service.LoadGameAsync("SaveSlot1");

    // Assert
    result.Should().BeSameAs(expectedSave);
    A.CallTo(() => repo.LoadAsync("SaveSlot1", A<CancellationToken>._)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public async Task LoadGameAsync_Should_Return_Null_When_Repo_Returns_Null()
  {
    // Arrange
    var repo = A.Fake<IGameSaveRepository>();
    A.CallTo(() => repo.LoadAsync("NotFound", A<CancellationToken>._)).Returns((GameSave?) null);

    var service = new SaveService(repo);

    // Act
    var result = await service.LoadGameAsync("NotFound");

    // Assert
    result.Should().BeNull();
  }
}
