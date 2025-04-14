using TextRpg.Domain;
using TextRpg.Infrastructure.JsonRepositories;

namespace TextRpg.Infrastructure.Tests.JsonRepositories;

public class GameSaveJsonRepositoryTests : IDisposable
{
  private readonly string _tempDir;

  public GameSaveJsonRepositoryTests()
  {
    _tempDir = Path.Combine(Path.GetTempPath(), "GameSaveTests_" + Guid.NewGuid());

    if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, recursive: true);

    Directory.CreateDirectory(_tempDir);
  }

  [Fact]
  public async Task SaveAsync_Then_LoadAsync_Should_Persist_GameSave_Correctly()
  {
    // Arrange
    var repo = new GameSaveJsonRepository(_tempDir);
    var player = Character.Create("PlayerOne");
    player.AddTraits([Guid.NewGuid(), Guid.NewGuid()]);
    var world = World.Create(DateTime.Now, [player]);
    var save = GameSave.Create("TestSave", player, world);

    // Act
    await repo.SaveAsync(save, CancellationToken.None);
    var loaded = await repo.LoadAsync("TestSave", CancellationToken.None);

    // Assert
    loaded.Should().NotBeNull();
    loaded.Id.Should().Be(save.Id);
    loaded.Name.Should().Be(save.Name);
    loaded.PlayerCharacterId.Should().Be(player.Id);
    loaded.World.Should().BeEquivalentTo(world);
    loaded.PlayerCharacter.Name.Should().Be(player.Name);
    loaded.PlayerCharacter.TraitsId.Should().BeEquivalentTo(player.TraitsId);
  }

  [Fact]
  public async Task LoadAsync_Should_Return_Null_If_File_Not_Found()
  {
    // Arrange
    var repo = new GameSaveJsonRepository(_tempDir);

    // Act
    var result = await repo.LoadAsync("DoesNotExist", CancellationToken.None);

    // Assert
    result.Should().BeNull();
  }

  [Fact]
  public void GetSavePath_Should_Return_Path_Ending_With_Saves()
  {
    // Act
    var path = GameSaveJsonRepository.GetSavePath();

    // Assert
    path.Should().NotBeNullOrWhiteSpace();
    path.Should().EndWith("Saves");
    Directory.Exists(Path.GetDirectoryName(path)).Should().BeTrue("Parent directory of save path should exist");
  }

  [Fact]
  public void GetSavePath_Should_Handle_Electron_Path_Correctly()
  {
    // Arrange
    var fakeExePath = Path.Combine("C:", "mygame", "resources", "app", "server");

    // Act
    var savePath = GameSaveJsonRepository.GetSavePath(fakeExePath);

    // Assert
    savePath.Should().Be(Path.Combine("C:", "mygame", "Saves"));
  }


  public void Dispose()
  {
    if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, recursive: true);
  }
}
