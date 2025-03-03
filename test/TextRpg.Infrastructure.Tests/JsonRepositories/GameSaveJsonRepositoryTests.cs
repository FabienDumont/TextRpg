using TextRpg.Domain;
using TextRpg.Domain.Tests.Helpers;
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
    var playerCharacter = CharacterHelper.GetBasicPlayerCharacter();
    playerCharacter.AddTraits([Guid.NewGuid(), Guid.NewGuid()]);
    var world = World.Create(DateTime.Now, [playerCharacter]);
    var save = GameSave.Create("TestSave", playerCharacter, world);

    // Act
    await repo.SaveAsync(save, CancellationToken.None);
    var path = Path.Combine(_tempDir, "TestSave.json");
    var json = await File.ReadAllTextAsync(path, CancellationToken.None);
    var loaded = repo.Load(json);

    // Assert
    loaded.Should().NotBeNull();
    loaded.Id.Should().Be(save.Id);
    loaded.Name.Should().Be(save.Name);
    loaded.PlayerCharacterId.Should().Be(playerCharacter.Id);
    loaded.World.Should().BeEquivalentTo(world);
    loaded.PlayerCharacter.Name.Should().Be(playerCharacter.Name);
    loaded.PlayerCharacter.TraitsId.Should().BeEquivalentTo(playerCharacter.TraitsId);
  }

  [Fact]
  public void Load_Should_Return_Null_If_Json_Is_Invalid()
  {
    // Arrange
    var repo = new GameSaveJsonRepository();
    var invalidJson = "{ not even close to valid json";

    // Act
    var result = repo.Load(invalidJson);

    // Assert
    result.Should().BeNull();
  }

  [Fact]
  public void GetSavePath_Should_Return_Path_Ending_With_Saves()
  {
    // Act

    var path = GameSaveJsonRepository.GetSavePath("CharacterName");

    // Assert
    path.Should().NotBeNullOrWhiteSpace();
    path.Should().EndWith(@"Saves\CharacterName");
  }

  [Fact]
  public void GetSavePath_Should_Handle_Electron_Path_Correctly()
  {
    // Arrange
    var fakeExePath = Path.Combine("C:", "mygame", "resources", "app", "server");

    // Act
    var savePath = GameSaveJsonRepository.GetSavePath("CharacterName", fakeExePath);

    // Assert
    savePath.Should().Be(Path.Combine("C:", "mygame", "Saves", "CharacterName"));
  }

  public void Dispose()
  {
    if (Directory.Exists(_tempDir)) Directory.Delete(_tempDir, recursive: true);
  }
}
