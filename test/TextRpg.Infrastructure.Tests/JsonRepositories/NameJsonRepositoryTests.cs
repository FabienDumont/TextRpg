using TextRpg.Infrastructure.JsonRepositories;

namespace TextRpg.Infrastructure.Tests.JsonRepositories;

public class NameJsonRepositoryTests
{
  #region Fields

  private readonly string _tempDir;

  #endregion

  #region Ctors

  public NameJsonRepositoryTests()
  {
    _tempDir = Path.Combine(Path.GetTempPath(), $"NameRepoTest_{Guid.NewGuid()}");
    Directory.CreateDirectory(_tempDir);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task GetFemaleNamesAsync_ShouldReturnNames_FromJsonFile()
  {
    // Arrange
    var path = Path.Combine(_tempDir, "FemaleNames.json");
    await File.WriteAllTextAsync(path, """ ["Alice", "Beth", "Clara"] """);

    var repo = new NameJsonRepository(_tempDir);

    // Act
    var names = await repo.GetFemaleNamesAsync();

    // Assert
    names.Should().BeEquivalentTo("Alice", "Beth", "Clara");
  }

  [Fact]
  public async Task GetMaleNamesAsync_ShouldReturnNames_FromJsonFile()
  {
    // Arrange
    var path = Path.Combine(_tempDir, "MaleNames.json");
    await File.WriteAllTextAsync(path, """ ["John", "Mike", "Steve"] """);

    var repo = new NameJsonRepository(_tempDir);

    // Act
    var names = await repo.GetMaleNamesAsync();

    // Assert
    names.Should().BeEquivalentTo("John", "Mike", "Steve");
  }

  #endregion
}
