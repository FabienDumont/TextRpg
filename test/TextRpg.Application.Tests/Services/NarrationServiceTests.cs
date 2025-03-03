using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;

namespace TextRpg.Application.Tests.Services;

public class NarrationServiceTests
{
  #region Fields

  private readonly INarrationRepository _repository = A.Fake<INarrationRepository>();
  private readonly NarrationService _service;

  #endregion

  #region Ctors

  public NarrationServiceTests()
  {
    _service = new NarrationService(_repository);
  }

  #endregion

  #region Tests

  [Fact]
  public async Task GetNarrationTextByKeyAsync_ShouldReturnText_WhenNarrationExists()
  {
    // Arrange
    const string key = "intro_scene";
    const string expectedText = "You enter the ancient ruins.";
    var narration = Narration.Load(Guid.NewGuid(), key, expectedText);

    A.CallTo(() => _repository.GetNarrationByKeyAsync(key, A<CancellationToken>._)).Returns(Task.FromResult(narration));

    // Act
    var result = await _service.GetNarrationTextByKeyAsync(key, CancellationToken.None);

    // Assert
    Assert.Equal(expectedText, result);
  }

  [Fact]
  public async Task GetNarrationTextByKeyAsync_ShouldThrow_WhenRepositoryThrows()
  {
    // Arrange
    const string key = "nonexistent_scene";

    A.CallTo(() => _repository.GetNarrationByKeyAsync(key, A<CancellationToken>._))
      .Throws(new InvalidOperationException("Narration not found"));

    // Act & Assert
    var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
      _service.GetNarrationTextByKeyAsync(key, CancellationToken.None)
    );

    Assert.Equal("Narration not found", exception.Message);
  }

  #endregion
}
