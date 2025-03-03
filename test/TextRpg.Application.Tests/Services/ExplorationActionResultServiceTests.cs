using TextRpg.Application.Repositories;
using TextRpg.Application.Services;
using TextRpg.Domain;
using TextRpg.Domain.Tests.Helpers;

namespace TextRpg.Application.Tests.Services;

public class ExplorationActionResultServiceTests
{
  #region Fields

  private readonly IExplorationActionResultRepository _repository = A.Fake<IExplorationActionResultRepository>();
  private readonly ExplorationActionResultService _service;

  #endregion

  #region Ctors

  public ExplorationActionResultServiceTests()
  {
    _service = new ExplorationActionResultService(_repository);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task GetExplorationActionResultAsync_ShouldReturnExpectedResult()
  {
    // Arrange
    var actionId = Guid.NewGuid();
    var character = CharacterHelper.GetBasicPlayerCharacter();
    var expectedResult = ExplorationActionResult.Load(Guid.NewGuid(), actionId, 10, 90, 5, 50, true, 15, -10);

    A.CallTo(() => _repository.GetByExplorationActionIdAsync(actionId, character, A<CancellationToken>._))
      .Returns(expectedResult);

    // Act
    var result = await _service.GetExplorationActionResultAsync(actionId, character, CancellationToken.None);

    // Assert
    Assert.Same(expectedResult, result);
    A.CallTo(() => _repository.GetByExplorationActionIdAsync(actionId, character, A<CancellationToken>._))
      .MustHaveHappenedOnceExactly();
  }

  #endregion
}
